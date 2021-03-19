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

        


        private void Btn_Login_Click_1(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(Txt_Username.Text))
            {


                if (!string.IsNullOrWhiteSpace(Pass_Password.Password))
                {
                    if (mClient.SetServerIPAddress(Txt_Ip.Text) && mClient.SetServerPort(Txt_Port.Text))
                    {

                         mClient.ConnettiAlServer();

                            if (mClient.ConnettiAlServer().IsCompleted)
                            {
                                MessageBox.Show("done");
                                 //MainWindow win2 = new MainWindow();
                            //win2.Show();
                            //this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Invalid IP o Port");
                            
                            }
                            
                          
                            //MainWindow win2 = new MainWindow();
                            //win2.Show();
                            //this.Hide();

                        


                        

                    }
                    else
                    {
                        MessageBox.Show("IP O PORT are not valid");
                        Txt_Port.Focus();
                    }

                    
                }
                else { MessageBox.Show("Password not valid");

                    Pass_Password.Focus();
                }
                

            }
            else {
                MessageBox.Show("Username not valid");
                Txt_Username.Focus();
            } 
           
        }


        
    }
}
