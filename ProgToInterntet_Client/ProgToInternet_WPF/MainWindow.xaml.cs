using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Net;

namespace ProgToInternet_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static IPAddress serverIp;
        static int serverPort = 8000;
        static IPEndPoint serverIpEndPoint;

        static Socket connectionSocket = new Socket(AddressFamily.InterNetwork,
                                                SocketType.Stream,
                                                ProtocolType.Tcp);
        public MainWindow()
        {
            serverIp = new IPAddress(new byte[] { 0, 0, 0, 0});
            serverIpEndPoint = new IPEndPoint(serverIp, serverPort);
            InitializeComponent();
        }
        private void connectButton_Click_1(object sender, RoutedEventArgs e)
        {
            try 
            {
                string stringIp = textBoxIP.Text;
                

                serverIp = IPAddress.Parse(stringIp);

                connectionSocket
                connectionSocket.Connect(serverIpEndPoint);

                MessageBox.Show(serverIp.ToString());
            }
            catch(Exception E)
            {
                MessageBox.Show("BREAK");
                MessageBox.Show(E.Message.ToString());
            }
            
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            string messageToServer = textBox1.Text;
            connectionSocket.Send(Encoding.Unicode.GetBytes(messageToServer));

            byte[] rxBuf = new byte[1024];
            connectionSocket.Receive(rxBuf);

            Console.WriteLine("Response from server: {0}", Encoding.Unicode.GetString(rxBuf));
        }
    }
}
