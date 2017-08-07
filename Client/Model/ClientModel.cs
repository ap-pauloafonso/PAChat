using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public class ClientModel: INotifyPropertyChanged
    {
        public ClientModel()
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public Socket Socket { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;

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


        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
