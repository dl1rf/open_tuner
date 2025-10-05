using Newtonsoft.Json;
using System;
using WebSocketSharp;
using System.Drawing;
using Serilog;
using System.Globalization;

namespace opentuner.MediaSources.Longmynd
{
    public partial class LongmyndSource
    {
        // ws interface
        private WebSocket controlWS;        // longmynd control ws websocket
        private WebSocket monitorWS;        // longmynd monitor ws websocket
        private bool controlDisconnect;
        private bool monitorDisconnect;
        private bool controlConnected;
        private bool monitorConnected;
        private bool controlClosed;
        private bool monitorClosed;

        private void WSSetFrequency(uint frequency, uint symbol_rate)
        {
            controlWS.Send("C" + (frequency - _settings.DefaultOffset).ToString() + "," + symbol_rate.ToString());
        }

        public void WSSetTS(string ip, int port)
        {
            Log.Information(ip.ToString() + " - " + port.ToString());
            controlWS.Send("U" + ip + ":" + port);
        }

        private void connectWebsockets()
        {

            string url = "ws://" + _settings.LongmyndWSHost + ":" + _settings.LongmyndWSPort.ToString() + "/ ";

            monitorWS = new WebSocket(url, "monitor");
            monitorWS.OnOpen += Monitorws_OnOpen;
            monitorWS.OnMessage += Monitorws_OnMessage;
            monitorWS.OnClose += Monitorws_OnClose;
            monitorWS.OnError += MonitorWS_OnError;
            monitorWS.ConnectAsync();
            monitorDisconnect = true;
            monitorConnected = false;
            monitorClosed = false;

            controlWS = new WebSocket(url, "control");
            controlWS.OnClose += Controlws_OnClose;
            controlWS.OnMessage += Controlws_OnMessage;
            controlWS.OnOpen += Controlws_OnOpen;
            controlWS.OnError += ControlWS_OnError;
            controlWS.ConnectAsync();
            controlDisconnect = true;
            controlConnected = false;
            controlClosed = false;
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
            if (controlConnected)
                _connected = true;
        }



        public void debug(string msg)
        {
            Log.Information(msg);
        }

        private void Controlws_OnOpen(object sender, EventArgs e)
        {
            debug("Success: Control WS Open");
            controlConnected = true;
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
}
