using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LibNetwork
{
    public class DataExchange
    {
        public static string ReceiveData(Socket socket)
        {
            byte[] bufferLength = new byte[4];
            byte[] buffer = new byte[2048];
            int bytes_read = 0;

            bytes_read += socket.Receive(bufferLength);

            if (bytes_read == 0)
            {
                throw new ConnectionFriendlyEndedException();
            }

            int length = BitConverter.ToInt32(bufferLength, 0);

            while (bytes_read < length)
            {
                bytes_read += socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);
            }

            return Encoding.ASCII.GetString(buffer, 0, bytes_read - 4);

        }

        public static void SendData(Socket socket, string data)
        {
            byte[] bufferLength = BitConverter.GetBytes(data.Length + 4);
            byte[] bufferData = Encoding.ASCII.GetBytes(data);

            byte[] sendBuffer = new byte[bufferLength.Length + bufferData.Length];

            Array.Copy(bufferLength, sendBuffer, bufferLength.Length);
            Array.Copy(bufferData, 0, sendBuffer, bufferLength.Length, bufferData.Length);

            socket.Send(sendBuffer, 0, sendBuffer.Length, SocketFlags.None);
        }
    }
}
