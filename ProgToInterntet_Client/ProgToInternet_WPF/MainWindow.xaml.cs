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
        Client _client;
        public MainWindow()
        {
            _client = new Client();
            InitializeComponent();
        }

        public string GetDateWithLogFormat()
        {
            return String.Format("[{0:s}]:", DateTime.Now.ToString());
        }
        private void connectButton_Click_1(object sender, RoutedEventArgs e)
        {
            try 
            {
                _client.ServerIp = textBoxIP.Text;
                if(_client.ConnectSocket())
                {
                    string timeString = DateTime.Now.ToString();
                    //TODO: не позорься, сделай формат
                    logListBox.Items.Add("["+timeString + "]: " + " connected to server " + _client.ServerIp.ToString());
                }
            }
            catch(Exception E)
            {
                MessageBox.Show("BREAK");
                MessageBox.Show(E.Message.ToString());
            }

        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] messageToServer = Encoding.Unicode.GetBytes(textBox1.Text);
            byte[] rxBuf = new byte[1024];

            if(_client.SendAndGetResponse(ref messageToServer, ref rxBuf))
            {
                string logString = String.Format("{0:s} \"{1:s}\" is sent to {2:s}", GetDateWithLogFormat(), textBox1.Text, _client.ServerIp.ToString());

                logListBox.Items.Add(logString);

                logString = String.Format("{0:s} response from {2:s} is \"{1:s}\"", GetDateWithLogFormat(), textBox1.Text, _client.ServerIp.ToString());
                logListBox.Items.Add(logString);


            }
            _client.CloseSocket();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string logString = String.Format("{0:s} Начата процедура поиска сервера ", GetDateWithLogFormat());
            logListBox.Items.Add(logString);

            Client findServerClient = new Client(8000) { ServerIp = HackIpServerTextBox.Text };

            if (findServerClient.ServerIp != null)
            {
                string[] ipOctetsStr = findServerClient.ServerIp.Split('.');

                int variableOctet = Convert.ToInt32(ipOctetsStr[3]);


                for (int i = variableOctet; i < 256; i++)
                {
                    string searchIpServerString = String.Format("{0:s}.{1:s}.{2:s}.{3:D}", ipOctetsStr[0], ipOctetsStr[1], ipOctetsStr[2], variableOctet);
                    findServerClient.ServerIp = searchIpServerString;

                    if (findServerClient.ConnectSocket())
                    {
                        logString = String.Format("{0:s} tried ip {1:s} - FOUND!", GetDateWithLogFormat(), searchIpServerString);
                        logListBox.Items.Add(logString);

                        break;
                    }
                    else
                    {
                        logString = String.Format("{0:s} tried ip {1:s} - fail!", GetDateWithLogFormat(), searchIpServerString);
                        logListBox.Items.Add(logString);
                    }
                    
                }
            }

        }
    }
}
