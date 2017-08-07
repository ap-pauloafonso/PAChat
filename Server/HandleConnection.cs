using LibMessages;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using LibNetwork;

namespace Server
{
    class HandleConnection
    {
        private ConnectionModel connection;
        private Dictionary<string, ConnectionModel> clientList;

        public HandleConnection(ConnectionModel connection, Dictionary<string, ConnectionModel> clientList)
        {
            this.connection = connection;
            this.clientList = clientList;
            Thread t = new Thread(ConnectionThread);
            t.Start();
        }
        private void ConnectionThread()
        {
            try
            {
                //get client nickname
                connection.nickName = DataExchange.ReceiveData(connection.socket);
                //checks if the nickname is valid
                if (ServerManager.Authenticate(connection.nickName, clientList))
                {
                    //broadcast the new client to all other clients on the list 
                    ServerManager.BroadcastStatus(connection.nickName, clientList,StatusEnum.Connected);
                    //add to list 
                    ServerManager.AddClientList(connection, clientList);
                    //send an <OK> response && client list
                    ServerManager.SendAuthenticateResponse(connection.socket, clientList, ResponseEnum.Sucess);

                    //listening and broadcasting to others clients
                    while (true)
                    {
                        string data = DataExchange.ReceiveData(connection.socket);
                        ServerManager.BroadcastMsg(connection.nickName, data, clientList);
                    }
                }
                else
                {
                    //send an  <ERROR> response to the client 
                    //this method will throw an InvalidNickNameException on the client side if the NickName is invalid
                    ServerManager.SendAuthenticateResponse(connection.socket, clientList, ResponseEnum.Error);
                    connection.socket.Close();
                }
            }
            catch (ConnectionFriendlyEndedException)
            {
                ServerManager.RemoveClientList(connection, clientList);
                ServerManager.BroadcastStatus(connection.nickName, clientList, StatusEnum.Disconnected);
                connection.socket.Shutdown(SocketShutdown.Both);
                connection.socket.Close();
            }
            catch (Exception)
            {
                ServerManager.RemoveClientList(connection, clientList);
                ServerManager.BroadcastStatus(connection.nickName, clientList, StatusEnum.Disconnected);
                connection.socket.Close();
            }

        }
    }
}
