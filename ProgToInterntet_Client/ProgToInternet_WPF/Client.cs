using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows;
using System.ComponentModel;

using System.Threading;
using System.Threading.Tasks;

namespace ProgToInternet_WPF
{
    class Client : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        IPAddress _serverIp;
        int _serverPort = 8000;
        IPEndPoint _serverIpEndPoint;

        Socket _connectionSocket;

        public Client(int serverPort = 8000)
        {
            _serverPort = serverPort;
            _connectionSocket = new Socket(AddressFamily.InterNetwork,
                                               SocketType.Stream,
                                               ProtocolType.Tcp);

            _serverIp = new IPAddress(new byte[] { 0, 0, 0, 0 });

            _serverIpEndPoint = new IPEndPoint(_serverIp, _serverPort);
        }

        public int ServerPort
        {
            get
            {
                return _serverPort;
            }
        }

        public string ServerIp
        {
            get
            {
                if (_serverIp != null)
                {
                    return _serverIp.ToString();
                }
                else
                {
                    MessageBox.Show("Попытка взятия IP-сервера : IP не задан");
                    return null;
                }
            }
            set
            {
                if (IPAddress.TryParse(value, out _serverIp))
                {
                    _serverIpEndPoint.Address = _serverIp;
                    OnPropertyChanged("server");
                }
                else
                {
                    MessageBox.Show("Входная строка не может преобразоваться в IP");
                }
            }
        }

        public bool ConnectSocket()
        {
            try
            {
                if (!_connectionSocket.Connected)
                {
                    _connectionSocket = new Socket(AddressFamily.InterNetwork,
                                               SocketType.Stream,
                                               ProtocolType.Tcp);

                    _connectionSocket.ReceiveTimeout = 1;
                    _connectionSocket.SendTimeout = 500;
                    _connectionSocket.Connect(_serverIpEndPoint);
                    return true;
                }
                else
                {
                    //MessageBox.Show("Already connected");
                }

                return false;
            }
            catch (Exception e)
            {
               // MessageBox.Show(e.Message.ToString());
                return false;
            }
            
        }

        public bool SendAndGetResponse(ref byte[] sendBuf, ref byte[] outBuf)
        {
            if (_connectionSocket.Connected)
            {
                _connectionSocket.Send(sendBuf);
                _connectionSocket.Receive(outBuf);
                return true;
            }
            else
            {
                MessageBox.Show("Socket is closed");
                return false;
            }
        }

        public bool CloseSocket()
        {
            if (_connectionSocket.Connected)
            {
                _connectionSocket.Close();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
