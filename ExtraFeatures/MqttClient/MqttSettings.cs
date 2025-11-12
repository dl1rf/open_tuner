using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opentuner.ExtraFeatures.MqttClient
{
    public class MqttManagerSettings
    {
        public string MqttBroker = "0.0.0.0";
        public int MqttPort = 1883;
        public bool MqttSendProperties = false;
    }
}
