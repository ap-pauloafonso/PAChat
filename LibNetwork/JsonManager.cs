using LibMessages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetwork
{
    public static class JsonManager
    {
        public static MessageTypeEnum FindGenericMessageType(string json)
        {
            Message genericMessage = JsonConvert.DeserializeObject<Message>(json);
            return genericMessage.MessageType;
        }

        public static string SerializeJson(Message message)
        {
            return JsonConvert.SerializeObject(message);
        }
    }
}
