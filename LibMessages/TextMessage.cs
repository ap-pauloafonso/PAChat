using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMessages
{
    public class TextMessage:Message
    {
        public TextMessage(string Client, string Text)
        {
            MessageType = MessageTypeEnum.TextMessage;
            this.Client = Client;
            this.Text = Text;
        }
        public string Client { get; set; }
        public string Text { get; set; }
    }
}
