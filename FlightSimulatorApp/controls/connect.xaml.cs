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
using System.Configuration;

namespace FlightSimulatorApp.controls
{
    /// <summary>
    /// Interaction logic for connect.xaml
    /// </summary>
    public partial class connect : UserControl
    {
        private IAppModel model;
        public connect()
        {
            InitializeComponent();
            //gets the model from app
            this.model = (Application.Current as App).Model;
        }
        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            int check = 0;
            //takes ip and port from app.config file
            int port = Int32.Parse(ConfigurationManager.AppSettings["port"].ToString());
            string ip = ConfigurationManager.AppSettings["ip"].ToString();
            try
            {
                model.connect(ip, port);
            }
            catch (Exception)
            {
                check = 1;
            }
            if (check == 0)
                model.start();
        }
        private void disconnectButton_Click(object sender, RoutedEventArgs e)
        {
            model.disconnect();
        }
        private void ipTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ipTextBox.Text != "127.0.0.1")
            {
                //change the ip in app.config file
                ConfigurationManager.AppSettings.Set("ip", ipTextBox.Text);
                ConfigurationManager.RefreshSection("ip");
            }
        }

        private void portTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (portTextBox.Text != "5402")
            {
                //change the ip in app.config file
                ConfigurationManager.AppSettings.Set("port", portTextBox.Text);
                ConfigurationManager.RefreshSection("port");
            }
        }
    }
}
