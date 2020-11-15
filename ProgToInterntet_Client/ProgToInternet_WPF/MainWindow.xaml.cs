﻿using System;
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
using System.Threading;
using System.Threading.Tasks;

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
            _client = new Client(logListBox);
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

                string timeString = DateTime.Now.ToString();

                logListBox.Items.Add("["+timeString + "]: " + " set server " + _client.ServerIp.ToString());
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message.ToString());
            }

        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] messageToServer = Encoding.UTF8.GetBytes(textBox1.Text);
            byte[] rxBuf = new byte[1024];

            try
            {
                if (_client.ConnectSocket() && _client.SendAndGetResponse(ref messageToServer, ref rxBuf))
                {
                    string logString = String.Format("{0:s} \"{1:s}\" >> {2:s}", GetDateWithLogFormat(), textBox1.Text, _client.ServerIp.ToString());

                    logListBox.Items.Add(logString);

                    string serverResponse = Encoding.UTF8.GetString(rxBuf, 0, rxBuf.Count());

                    serverResponse = serverResponse.Substring(0, serverResponse.IndexOf('\0'));

                    logString = String.Format("{0:s} \"{1:s}\" << {2:s}", GetDateWithLogFormat(), serverResponse, _client.ServerIp.ToString());
                    logListBox.Items.Add(logString);


                }
                _client.CloseSocket();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
