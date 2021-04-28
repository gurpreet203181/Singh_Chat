using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using Singh_SocketAsyncLib;

namespace Singh_Chat_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AsyncSocketClient mClient;
        public MainWindow(AsyncSocketClient LoggedClient)
        {
            InitializeComponent();
            mClient = LoggedClient;
            mClient.OnNewMessage += client_onNewMessage;

        }

        private void client_onNewMessage(object sender, EventArgs e)
        {
            lst_msg.ItemsSource = mClient.Messaggi;
           lst_msg.Items.Refresh();
        }

       

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btn_invia_Click(object sender, RoutedEventArgs e)
        {
            mClient.Invia(txt_invia.Text);
            txt_invia.Text = "";
        }
    }
}
