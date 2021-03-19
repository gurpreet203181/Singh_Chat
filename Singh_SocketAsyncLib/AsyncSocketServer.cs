using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
namespace Singh_SocketAsyncLib
{
    public class AsyncSocketServer
    {
        IPAddress mIP;
        int mPort;
        TcpListener mServer;
        List<TcpClient> mClients;

        public AsyncSocketServer()
        {
            mClients = new List<TcpClient>();
        }

        // Server inizia as ascoltare
        public async void InAscolto(IPAddress ipaddr = null, int port = 23000)
        {
            //controlli generali
            if (ipaddr == null)
            {
                ipaddr = IPAddress.Any;
            }
            if (port < 0 || port > 65535)
            {
                port = 23000;
            }

            mIP = ipaddr;
            mPort = port;

            mServer = new TcpListener(mIP, mPort);

            Console.WriteLine("Server in ascolto su IP: {0} - Porta: {1}"
                                 , mIP.ToString(), mPort.ToString());
            mServer.Start();

            Console.WriteLine("Server avviato.");
            while (true)
            {
                TcpClient client = await mServer.AcceptTcpClientAsync();

                mClients.Add(client);

                Console.WriteLine("Client Connessi: {0}. Client Connesso: {1}",
                    mClients.Count, client.Client.RemoteEndPoint);
                
                RiceviMessaggio(client);
            }

        }
       
        //metodo per inviare metodo Inviatutti ogni 10sec
      

        public async void RiceviMessaggio(TcpClient client)
        {
            NetworkStream stream = null;
            StreamReader reader = null;
            try
            {
                stream = client.GetStream();
                reader = new StreamReader(stream);
                char[] buff = new char[512];
                byte[] buffsend = null;
                int nBytes = 0;
                while (true)
                {
                    Console.WriteLine("In attesa di un messaggio");
                    //ricezione messaggio asincrono
                    nBytes = await reader.ReadAsync(buff, 0, buff.Length);
                    if (nBytes == 0)
                    {
                        RimuoviClient(client);
                        Console.WriteLine("Client Disconnesso"); 
                        break; 
                    }

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Errore: " + ex.Message);
            }


        }
        public async void RegistraClient(TcpClient client)
        {
            NetworkStream stream = null;
            StreamReader reader = null;
            try
            {
                stream = client.GetStream();
                reader = new StreamReader(stream);
                char[] buff = new char[512];
                int nBytes = 0;
               
                    Console.WriteLine("In attesa di un messaggio");
                    //ricezione messaggio asincrono
                    nBytes = await reader.ReadAsync(buff, 0, buff.Length);

                string recvText = new string(buff);
                NicknameModel newClient = new NicknameModel();
                newClient.NickName = recvText;
                newClient.Client = client;

                

            }
            catch (Exception ex)
            {

                Console.WriteLine("Errore: " + ex.Message);
            }


        }
        private void RimuoviClient(TcpClient client)
        {
            if (mClients.Contains(client))
            {
                mClients.Remove(client);
            }
        }
        

        public void InviaATutti()
        {
            string dateTime = DateTime.Now.ToString("dd MM yyyy,hh:mm:ss");
            try
            {
                foreach (TcpClient client in mClients)
                {
                    byte[] buff = Encoding.ASCII.GetBytes($"{dateTime}");
                    client.GetStream().WriteAsync(buff, 0, buff.Length);

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Errore: " + ex.Message);
            }

        }
        public void Disconnetti()
        {
            try
            {
                foreach (TcpClient client in mClients)
                {
                    client.Close();
                    RimuoviClient(client);
                }
                mServer.Stop();


            }
            catch (Exception ex)
            {

                Console.WriteLine("Errore: " + ex.Message);
            }

        }

    }
    }
