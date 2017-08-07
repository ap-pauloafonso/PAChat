using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMessages
{
    public static class JsonManager
    {
        public static Tuple<MessageTypeEnum, Message> DeserializeJson(string json)
        {
            DataWrapper deserializedWrapper = JsonConvert.DeserializeObject<DataWrapper>(json);

            switch (deserializedWrapper.DataType)
            {
                case MessageTypeEnum.AlertMessage:
                    return new Tuple<MessageTypeEnum, Message>(deserializedWrapper.DataType, JsonConvert.DeserializeObject<AlertMessage>(deserializedWrapper.JsonData));
                case MessageTypeEnum.TextMessage:
                    return new Tuple<MessageTypeEnum, Message>(deserializedWrapper.DataType, JsonConvert.DeserializeObject<TextMessage>(deserializedWrapper.JsonData));
                default:
                    return null;
            }
        }

        public static string SerializeJson(Message message)
        {
            
            return  JsonConvert.SerializeObject(message);
        }
    }

}
