using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Interop;
using Vortice.MediaFoundation;
using WebSocketSharp;

namespace opentuner.MediaSources.WinterHill
{
    public partial class WinterHillSource
    {
        // ws interface
        private WebSocket controlWS;        // longmynd control ws websocket
        private WebSocket monitorWS;        // longmynd monitor ws websocket
        private static Timer PingTimer;
        private bool controlDisconnect;
        private bool monitorDisconnect;
        private bool controlConnected;
        private bool monitorConnected;
        private bool controlClosed;
        private bool monitorClosed;
        private bool controlReconnect;
        private bool monitorReconnect;
        private bool controlPingResult;
        private bool monitorPingResult;

        private void WSSetFrequency(int device, int freq, int sr)
        {           
            controlWS.Send("F" + (device + 1).ToString() + "," + freq + "," + sr.ToString() + "," + _current_offset[device]);
        }

        private void WSSetTS(int device)
        {
            string wh_command = ("U" + (device + 1).ToString() + "," + _LocalIp.ToString());
            Log.Debug(wh_command);
            controlWS.Send(wh_command);
        }

        private void WSResetTS(int device)
        {
            string wh_command = ("U" + (device + 1).ToString() + "," + "127.0.0.1");
            Log.Debug(wh_command);
            controlWS?.Send(wh_command);
            Task.Delay(100);   // delay to allow WinterHill to get to stable state
        }

        private void connectWebsockets()
        {
            string url = "ws://" + _settings.WinterHillWSHost + ":" + _settings.WinterHillWSPort.ToString() + "/ ";

            monitorWS = new WebSocket(url, "monitor");
            monitorWS.OnOpen += Monitorws_OnOpen;
            monitorWS.OnMessage += Monitorws_OnMessage;
            monitorWS.OnClose += Monitorws_OnClose;
            monitorWS.OnError += MonitorWS_OnError;
            monitorDisconnect = true;
            monitorConnected = false;
            monitorClosed = false;
            monitorReconnect = false;
            monitorWS.ConnectAsync();

            controlWS = new WebSocket(url, "control");
            controlWS.OnClose += Controlws_OnClose;
            controlWS.OnMessage += Controlws_OnMessage;
            controlWS.OnOpen += Controlws_OnOpen;
            controlWS.OnError += ControlWS_OnError;
            controlDisconnect = true;
            controlConnected = false;
            controlClosed = false;
            controlReconnect = false;
            controlWS.ConnectAsync();
        }

        private void startPingTimer()
        {
            // Create a timer with a two second interval.
            PingTimer = new Timer(10000);
            // Hook up the Elapsed event for the timer. 
            PingTimer.Elapsed += OnStartPingEvent;
            PingTimer.AutoReset = true;
            PingTimer.Enabled = true;
        }

        private void stopPingTimer()
        {
            PingTimer?.Stop();
        }

        private void OnStartPingEvent(object sender, ElapsedEventArgs e)
        {
            if (controlConnected == true && controlReconnect == true &&
                monitorConnected == true && monitorReconnect == true)
            {
                controlReconnect = false;
                monitorReconnect = false;
                PingTimer.Stop();
                Task.Delay(10000);   // delay to allow WinterHill to get to stable state
                ReStart();
                PingTimer.Start();
            }
            if (controlConnected == true)
                controlPingResult = controlWS.Ping();
            if (monitorConnected == true)
                monitorPingResult = monitorWS.Ping();
            if (controlPingResult == false || monitorPingResult == false)
            {
                if (controlPingResult == false && controlReconnect == false ||
                    monitorPingResult == false && monitorReconnect == false)
                {
                    debug("Ping Error");
                    controlReconnect = true;
                    controlConnected = false;
                    controlWS?.Close();

                    monitorReconnect = true;
                    monitorConnected = false;
                    monitorWS?.Close();
                    _Ready = false;
                }
            }
        }

        private void ControlWS_OnError(object sender, ErrorEventArgs e)
        {
            debug("ControlWS_OnError: " + e.ToString());
        }

        private void MonitorWS_OnError(object sender, ErrorEventArgs e)
        {
            debug("MonitorWS_OnError: " + e.ToString());
        }

        private void Monitorws_OnOpen(object sender, EventArgs e)
        {
            debug("Success: Monitor WS Open");
            monitorConnected = true;
            if (monitorReconnect)
            {
                debug("Monitor WS Reconnected");
            }
        }

        public void debug(string msg)
        {
            Log.Information(msg);
        }

        private void Controlws_OnOpen(object sender, EventArgs e)
        {
            debug("Success: Control WS Open");
            controlConnected = true;
            if (controlReconnect)
            {
                debug("Control WS Reconnected");
            }
        }


        private void Controlws_OnMessage(object sender, MessageEventArgs e)
        {
        }

        private void Controlws_OnClose(object sender, CloseEventArgs e)
        {
            if (!controlDisconnect)
            {
                debug("Error: Control WS Closed - Check WS IP");
                debug("Attempting to reconnect...");
                controlReconnect = true;
                controlWS.ConnectAsync();
            }
            else
            {
                controlClosed = true;
                debug("Control WS Closed");
            }
        }

        private void Monitorws_OnClose(object sender, CloseEventArgs e)
        {
            if (!monitorDisconnect)
            {
                debug("Error: Monitor WS Closed - Check WS IP");
                debug("Attempting to reconnect...");
                monitorReconnect = true;
                monitorWS.ConnectAsync();
            }
            else
            {
                monitorClosed = true;
                debug("Monitor WS Closed");
            }
        }

        private void Monitorws_OnMessage(object sender, MessageEventArgs e)
        {
            monitorMessage mm = JsonConvert.DeserializeObject<monitorMessage>(e.Data);
            UpdateInfo(mm);
        }

        public void DisconnectWebsockets()
        {
            _connected = false;
            
            if (monitorWS != null)
            {
                if (monitorWS.IsAlive)
                {
                    monitorDisconnect = true;
                    monitorWS?.Close();
                }
            }

            if (controlWS != null)
            {
                if (controlWS.IsAlive)
                {
                    controlDisconnect = true;
                    controlWS?.Close();
                }
            }
        }
    }

    [Serializable]
    public class ReceiverMessage
    {
        public int rx;
        public int scanstate;
        public string service_name;
        public string service_provider_name;
        public string mer;
        public string dbmargin;
        public string frequency;
        public string symbol_rate;
        public string modcod;
        public string null_percentage;
        public string video_type;
        public string audio_type;
        public string ts_addr;
        public string ts_port;
        public int rfport;
    }


    [Serializable]
    public class monitorMessage
    {
        public string type;
        public double timestamp;
        public ReceiverMessage[] rx;
    }
}
