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
using System.Windows.Shapes;
using Singh_SocketAsyncLib;

namespace Singh_Chat_Client
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
            AsyncSocketClient mClient;

        public Login()
        {
            InitializeComponent();

            mClient = new AsyncSocketClient();
        }

        


        private async void Btn_Login_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Controlli();// metodo per controllare se username , password , ip, porta è valida
                            // e passa valore di porta e ip address all lib socketclient
                await mClient.ConnettiAlServer();
                
                if (mClient.IsConnected()) //metodo per vedere se il client è connessio all server
                {
                    MessageBox.Show("connected");

                     mClient.Invia(Txt_Username.Text);
                     MainWindow win2 = new MainWindow(mClient);
                     win2.Show();
                     this.Close();


                }
                else Lbl_errore.Content="Failed to connect to server check ip o port";




               



            }
            catch (Exception ex)
            {
                Lbl_errore.Content=ex.Message;
            }
           
        }



        private void Controlli()
        {
            if (string.IsNullOrWhiteSpace(Txt_Username.Text))
            throw new Exception("Username not valid");
            
            if (string.IsNullOrWhiteSpace(Pass_Password.Password)) 
                throw new Exception("Password not valid");
            
            if (mClient.SetServerIPAddress(Txt_Ip.Text) && mClient.SetServerPort(Txt_Port.Text)) { }//controlla e passa valore
               else throw new Exception("IP O PORT are not valid");

        }
    }
}
