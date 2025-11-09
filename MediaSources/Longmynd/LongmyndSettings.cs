using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opentuner.MediaSources.Longmynd
{
    public class LongmyndSettings
    {
        // general
        public byte DefaultInterface = 0;   // 0 = ws 1 = mqtt
        public uint RFPort = 0;
        public int TS_Port = 1234;

        // websocket
        public string LongmyndWSHost = "0.0.0.0";
        public int LongmyndWSPort = 8080;

        // mqtt
        public string LongmyndMqttHost = "0.0.0.0";
        public int LongmyndMqttPort = 1883;
        public string CmdTopic = "cmd/longmynd/";

        public uint DefaultFrequency = 10491500;
        public uint DefaultOffset = 9750000;
        public uint DefaultSR = 1500;
        public uint DefaultVolume = 50;
        public bool DefaultMuted = true;
        public bool DefaultUDPStreaming = false;
    }
}
