using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Singh_SocketAsyncLib;

namespace Singh_Chat_Server
{
   public class Program
    {
        static void Main(string[] args)
        {
           
            AsyncSocketServer mServer;
            mServer 
                = new AsyncSocketServer();
            mServer.InAscolto();

            Console.ReadLine();
        }
    }
}
