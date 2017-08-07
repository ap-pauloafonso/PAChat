using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LibMessages;
using Newtonsoft.Json;
using LibNetwork;
using System.IO;

namespace Server
{
    class ServerManager
    {
        private static readonly object _lock = new object();


        public static bool Authenticate(string nick, Dictionary<string, ConnectionModel> clientlist)
        {
            if (!clientlist.ContainsKey(nick) && !string.IsNullOrEmpty(nick))
                return true;

            else
                return false;

        }
        public static void AddClientList(ConnectionModel connection, Dictionary<string, ConnectionModel> clientlist)
        {
            lock (_lock) clientlist.Add(connection.nickName, connection);
        }
        public static void RemoveClientList(ConnectionModel connection, Dictionary<string, ConnectionModel> clientlist)
        {
            lock (_lock) clientlist.Remove(connection.nickName);
        }


        public static void SendAuthenticateResponse(Socket socket, Dictionary<string, ConnectionModel> clientList, ResponseEnum response)
        {
            if (response == ResponseEnum.Sucess)
            {
                AuthenticateResponse authenticate = new AuthenticateResponse(ResponseEnum.Sucess, clientList.Keys.ToList());
                string serialized= JsonConvert.SerializeObject(authenticate);
                DataExchange.SendData(socket, serialized);
                Console.WriteLine(serialized);
            }


            else
            {
                AuthenticateResponse authenticate = new AuthenticateResponse(ResponseEnum.Error, null);
                string serialized = JsonConvert.SerializeObject(authenticate);
                DataExchange.SendData(socket, serialized);
                Console.WriteLine(serialized);
            }
        }

        public static void BroadcastMsg(string nickName, string msg, Dictionary<string, ConnectionModel> clientlist)
        {

            lock (_lock)
            {
                foreach (ConnectionModel client in clientlist.Values)
                {
                    DataExchange.SendData(client.socket,msg);
                }
            }
            Console.WriteLine(msg);

        }


        public static void BroadcastStatus(string nickName, Dictionary<string, ConnectionModel> clientlist, StatusEnum status)
        {

            AlertMessage alert = new AlertMessage(nickName, status);


            string serialized = JsonConvert.SerializeObject(alert);

            lock (_lock)
            {
                foreach (ConnectionModel client in clientlist.Values)
                {
                    DataExchange.SendData(client.socket, serialized);
                }
            }

            Console.WriteLine(serialized);
        }
    }
}
