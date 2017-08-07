using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using LibNetwork;
using LibMessages;

namespace Client.Model
{
    public class ChatModel : INotifyPropertyChanged
    {
        public event EventHandler InvalidNickNameEvent;

        public ChatModel()
        {
            Client = new ClientModel() { Ip = "192.168.0.107", Port = "7000", NickName = "Paulo"};
            ClientList = new ObservableCollection<string>();
            MessageList = new ObservableCollection<Message>();
        }

        private ClientModel _Client;
        public ClientModel Client
        {
            get
            {
                return _Client;
            }
            set
            {
                _Client = value;
                OnPropertyChange("Client");
            }
        }

        public ObservableCollection<Message> MessageList { get; set; }

        public ObservableCollection<string> ClientList { get; set; }

        public void StartConnection()
        {
            Client.Socket.Connect(IPAddress.Parse(Client.Ip), Int32.Parse(Client.Port));
            Thread t = new Thread(() => ConnectionThread())
            {
                IsBackground = true
            };
            t.Start();
        }

        private void ConnectionThread()
        {
            try
            {
                DataExchange.SendData(Client.Socket, Client.NickName);

                //If authenticate fail this method will throw InvalidNickNameException, otherwise it will return a list of online users
                string[] clients = ClientManager.GetAuthenticateResponse(Client.Socket);
                
                // self-connect alert 
                Application.Current.Dispatcher.Invoke(() => MessageList.Add(new AlertMessage(Client.NickName, StatusEnum.Connected)));
                
                //add clients online to the list 
                foreach (string s in clients)
                {
                    Application.Current.Dispatcher.Invoke(() => ClientList.Add(s));
                }

                //listen to server DATA
                while (true)
                {
                    string data = DataExchange.ReceiveData(Client.Socket);
                    ClientManager.InterpretData(data, ClientList, MessageList);
                }

            }
            catch (InvalidNickNameException) // nick name already exist
            {
                InvalidNickNameEvent(this, new EventArgs()); //fires the event to viewmodel
                Client.Socket.Close();
                Reset();

            }
            catch (ConnectionFriendlyEndedException) //friendly disconnect 
            {
                Client.Socket.Close();
                Reset();
            }
            catch (Exception e) // unexpected disconnect
            {
                string text = e.Message;
            }
        }

        public void SendMessage(string data)
        {
            DataExchange.SendData(Client.Socket, data);
        }

        public void Disconnect()
        {
            Client.Socket.Shutdown(SocketShutdown.Send);
        }

        public void Reset()
        {
            Client.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Client.NickName = "";
            Client.Ip = "192.168.0.107";
            Client.Port = "7000";
            Application.Current.Dispatcher.Invoke(() => ClientList.Clear());
            Application.Current.Dispatcher.Invoke(() => MessageList.Clear());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
