using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ProgToInterntet_Client
{
    class Program
    {
        static IPAddress serverIp = new IPAddress(new byte[] { 127, 0, 0, 1 });
        static int serverPort = 8000;

        static IPEndPoint serverIpEndPoint = new IPEndPoint(serverIp, serverPort);

        static Socket connectionSocket = new Socket(AddressFamily.InterNetwork,
                                                SocketType.Stream,
                                                ProtocolType.Tcp);

        static void Main(string[] args)
        {
            try
            {
                connectionSocket.Connect(serverIpEndPoint);

                string messageToServer = "hello";
                connectionSocket.Send(Encoding.Unicode.GetBytes(messageToServer));

                byte[] rxBuf = new byte[1024];
                connectionSocket.Receive(rxBuf);

                Console.WriteLine("Response from server: {0}", Encoding.Unicode.GetString(rxBuf));
                connectionSocket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            finally
            {
                connectionSocket.Close();
            }


        }
    }
}
