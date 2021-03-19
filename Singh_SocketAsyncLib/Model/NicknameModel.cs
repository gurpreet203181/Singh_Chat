using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Singh_SocketAsyncLib
{
    public class NicknameModel
    {
        public String NickName { get; set; }
        public  TcpClient Client { get; set; }
    }
}
