using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;
using Sockets;
using System.IO;

namespace SocketOnlineServer
{
    public class UserConnector
    {
        public ITcpSocketClient client { get; private set; }
        public BinaryReader br { get; private set; }
        public BinaryWriter bw { get; private set; }
        public string userName { get; set; }
        public string userId { get; set; }

        public UserConnector(ITcpSocketClient client)
        {
            this.client = client;
            
            br = new BinaryReader(client.ReadStream);
            bw = new BinaryWriter(client.WriteStream);
        }

        public void Close()
        {
            br.Close();
            bw.Close();
            client.DisconnectAsync().Wait();
            client.Dispose();
        }
    }
}
