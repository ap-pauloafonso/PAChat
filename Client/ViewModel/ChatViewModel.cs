using Client.Model;
using LibMessages;
using LibNetwork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModel
{
    public class ChatViewModel : INotifyPropertyChanged
    {

        public ChatViewModel()
        {
            Connection = new ChatModel();
            BtConnect = new Command(Connect);
            BtSend = new Command(Send);
            BtDisconnect = new Command(Disconnect);
            Connection.InvalidNickNameEvent += InvalidNickNameAlert;
        }

        public enum UiStatus
        {
            Connected,
            Disconnected
        }

        public ICommand BtConnect { get; set; }
        public ICommand BtSend { get; set; }
        public ICommand BtDisconnect { get; set; }

        private ChatModel _connection;
        public ChatModel Connection
        {
            get
            {
                return _connection;
            }
            set
            {
                _connection = value;
                OnPropertyChange("Connection");
            }
        }

        private string _txtSend;
        public string TxtSend
        {
            get
            {
                return _txtSend;
            }
            set
            {
                _txtSend = value;

                OnPropertyChange("TxtSend");
            }
        }



        public void InvalidNickNameAlert(object sender, EventArgs e)
        {
            MessageBox.Show("ERROR INVALID NICKNAME");
            ChangeUI(UiStatus.Disconnected);
        }



        public void Disconnect(object parameter)
        {
            Connection.Disconnect();
            ChangeUI(UiStatus.Disconnected);
        }
        public void Send(object paramter)
        {
            TextMessage text = new TextMessage(Connection.Client.NickName, TxtSend);
            string serialized = JsonManager.SerializeJson(text);
            Connection.SendMessage(serialized);
            TxtSend = "";
        }
        public void Connect(object paramter)
        {
            try
            {
                Connection.StartConnection();
                ChangeUI(UiStatus.Connected);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }


        public void ChangeUI(UiStatus status)
        {
            if (status == UiStatus.Connected)
            {
                TxtIpIsEnabled = false;
                TxtPortIsEnabled = false;
                TxtNickIsEnabled = false;
                BtConnectIsEnabled = false;

                TxtChatIsEnabled = true;
                TxtSendIsEnabled = true;
                TxtClientsIsEnabled = true;
                BtDisconnectIsEnabled = true;
            }
            else
            {
                TxtIpIsEnabled = true;
                TxtPortIsEnabled = true;
                TxtNickIsEnabled = true;
                BtConnectIsEnabled = true;

                TxtChatIsEnabled = false;
                TxtSendIsEnabled = false;
                TxtClientsIsEnabled = false;
                BtDisconnectIsEnabled = false;

            }
        }

        private bool _btConnectIsEnabled = true;
        public bool BtConnectIsEnabled
        {
            get
            {
                return _btConnectIsEnabled;
            }
            set
            {
                _btConnectIsEnabled = value;
                OnPropertyChange("BtConnectIsEnabled");
            }
        }


        private bool _btDisconnectIsEnabled = false;
        public bool BtDisconnectIsEnabled
        {
            get
            {
                return _btDisconnectIsEnabled;
            }
            set
            {
                _btDisconnectIsEnabled = value;
                OnPropertyChange("BtDisconnectIsEnabled");
            }
        }


        private bool _txtChatIsEnabled = false;
        public bool TxtChatIsEnabled
        {
            get
            {
                return _txtChatIsEnabled;
            }
            set
            {
                _txtChatIsEnabled = value;
                OnPropertyChange("TxtChatIsEnabled");
            }
        }

        private bool _txtClientsIsEnabled = false;
        public bool TxtClientsIsEnabled
        {
            get
            {
                return _txtClientsIsEnabled;
            }
            set
            {
                _txtClientsIsEnabled = value;
                OnPropertyChange("TxtClientsIsEnabled");
            }
        }

        private bool _txtSendIsEnabled = false;
        public bool TxtSendIsEnabled
        {
            get
            {
                return _txtSendIsEnabled;
            }
            set
            {
                _txtSendIsEnabled = value;
                OnPropertyChange("TxtSendIsEnabled");
            }
        }

        private bool _txtIpIsEnabled = true;
        public bool TxtIpIsEnabled
        {
            get
            {
                return _txtIpIsEnabled;
            }
            set
            {
                _txtIpIsEnabled = value;
                OnPropertyChange("TxtIpIsEnabled");
            }
        }

        private bool _txtPortIsEnabled = true;
        public bool TxtPortIsEnabled
        {
            get
            {
                return _txtPortIsEnabled;
            }
            set
            {
                _txtPortIsEnabled = value;
                OnPropertyChange("TxtPortIsEnabled");
            }
        }

        private bool _txtNickIsEnabled = true;
        public bool TxtNickIsEnabled
        {
            get
            {
                return _txtNickIsEnabled;
            }
            set
            {
                _txtNickIsEnabled = value;
                OnPropertyChange("TxtNickIsEnabled");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
