using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProgToInternet_WPF
{
    //В дальнейшем можно создать поток логера - выводить логи в любой вывод
    class Loger
    {
        private ListBox logerListBox;

        public Loger(ListBox listBox)
        {            
            logerListBox = listBox;
        }
        private string GetDateWithLogFormat()
        {
            string logString = String.Format("[{0:s}]: ", DateTime.Now.ToString());
            return logString;
        }

        public void Print(string message)
        {
            string logString = GetDateWithLogFormat() + message;
            ListBoxItem listBoxItem = new ListBoxItem();

            logerListBox.Items.Add(logString);
            logerListBox.ItemsSource = logerListBox.ItemsSource;
        }

    }
}
