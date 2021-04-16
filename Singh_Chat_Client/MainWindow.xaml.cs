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
            Lst_Message.ItemsSource = mClient.Messaggi;
            Lst_Message.Items.Refresh();
        }

        private void Btn_Prova_Click(object sender, RoutedEventArgs e)
        {
            mClient.Invia(Txt_Prova.Text);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
