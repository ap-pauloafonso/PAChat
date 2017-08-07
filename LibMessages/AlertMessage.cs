using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMessages
{
    public class AlertMessage: Message
    {
        public AlertMessage(string Client, StatusEnum status)
        {
            MessageType = MessageTypeEnum.AlertMessage;
            this.Status = status;
            this.Client = Client;
        }
        public string Client { get; set; }
        public StatusEnum Status { get; set; }
    }
}
