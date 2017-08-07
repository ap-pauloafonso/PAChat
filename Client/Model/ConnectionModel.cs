using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    class ConnectionModel : INotifyPropertyChanged
    {
        public ConnectionModel()
        {
            
        }
        private Socket socket;

        private string _nickName;
        public string NickName
        {
            get
            {
                return _nickName;
            }
            set
            {
                _nickName = value;
                OnPropertyChange("NickName");
            }
        }

        private string _ip;
        public string Ip
        {
            get
            {
                return _ip;

            }
            set
            {
                _ip = value;
                OnPropertyChange("Ip");
            }
        }

        private string _port;
        public string Port
        {
            get
            {
                return _port;

            }
            set
            {
                _port = value;
                OnPropertyChange("Port");
            }
        }


        public ObservableCollection<string> MessageList { get; set; }

        public ObservableCollection<string> ClientList { get; set; }


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
