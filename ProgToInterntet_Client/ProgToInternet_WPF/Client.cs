using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows;

namespace ProgToInternet_WPF
{
    class Client
    {
        IPAddress _serverIp;
        int _serverPort;
        IPEndPoint _serverIpEndPoint;

        Socket _connectionSocket;

        public Client(int serverPort = 8000)
        {
            _serverPort = serverPort;
            _connectionSocket = new Socket(AddressFamily.InterNetwork,
                                               SocketType.Stream,
                                               ProtocolType.Tcp);

            _serverIp = new IPAddress(new byte[] {0,0,0,0});
        }

        public string ServerIp
        {
            get
            {
                if (_serverIp != null)
                    return _serverIp.ToString();
                else
                {
                    MessageBox.Show("Попытка взятия IP-сервера провалена: IP не задан");
                    return "0.0.0.0";
                }
            }
            set
            {
                _serverIp = IPAddress.Parse(value);
            }
        }
    }
}
