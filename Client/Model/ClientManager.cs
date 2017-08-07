using LibMessages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LibNetwork;
using System.Dynamic;
using Newtonsoft.Json.Converters;

namespace Client.Model
{
    class ClientManager
    {
        public static string[] GetAuthenticateResponse(Socket socket)
        {
            string response = DataExchange.ReceiveData(socket);

            AuthenticateResponse deserialized = JsonConvert.DeserializeObject<AuthenticateResponse>(response);

            if (deserialized.Response == ResponseEnum.Error)
            {
                throw new InvalidNickNameException();
            }
            else
            {
                return deserialized.ClientList.ToArray();
            }

        }

        public static void InterpretData(string data, ObservableCollection<string> clientList, ObservableCollection<Message> messageList)
        {
            //Finds the type of the generic message
            MessageTypeEnum type = JsonManager.FindGenericMessageType(data);

            //deserialize and take an action according to the type
            switch (type)
            {
                case MessageTypeEnum.AlertMessage:
                    AlertMessage alert = JsonConvert.DeserializeObject<AlertMessage>(data);
                    AlertAction(alert, clientList, messageList);
                    break;
                case MessageTypeEnum.TextMessage:
                    TextMessage text = JsonConvert.DeserializeObject<TextMessage>(data);
                    TextAction(text,messageList);
                    break;
            }
        }

        public static void AlertAction(AlertMessage alert, ObservableCollection<string> clientList, ObservableCollection<Message> messageList)
        {
            if (alert.Status == StatusEnum.Connected)
            { 
                Application.Current.Dispatcher.Invoke(() => clientList.Add(alert.Client));
                Application.Current.Dispatcher.Invoke(() => messageList.Add(alert));
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() => clientList.Remove(alert.Client));
                Application.Current.Dispatcher.Invoke(() => messageList.Add(alert));
            }

        }
        public static void TextAction(TextMessage text, ObservableCollection<Message> messageList)
        {
            Application.Current.Dispatcher.Invoke(() => messageList.Add(text));
        }
    }
}
