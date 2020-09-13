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
                //TODO: не позорься, сделай формат
                
                string logString = "[" + DateTime.Now.ToString() + "]: " + " \"" + textBox1.Text +
                    "\"" + " is sent to " + _client.ServerIp.ToString();
                logListBox.Items.Add(logString);

                logString = "[" + DateTime.Now.ToString() + "]: " + " response from " + _client.ServerIp.ToString() +
                    "\"" + Encoding.Unicode.GetString(rxBuf) + "\"";
                logListBox.Items.Add(logString);


            }
            _client.CloseSocket();
        }
    }
}
