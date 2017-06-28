using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sockets;
using Sockets.Plugin;
using System.Diagnostics;
using System.Threading;
using static Newtonsoft.Json.JsonConvert;

namespace SocketOnlineServer
{
    public class SocketServerConnector
    {
        public static async Task openLinsten() {
            var listenPort = 11000;
            var listener = new TcpSocketListener();
            Debug.Write("Open Linsten!\n");
            // when we get connections, read byte-by-byte from the socket's read stream
            listener.ConnectionReceived += async (sender, args) =>
            {
                Console.WriteLine("we have a new client sign in.");
                UserConnector userConnector = new UserConnector(args.SocketClient);
                    await Task.Factory.StartNew(async() =>
                    {
                        foreach (var m in userConnector.ReadStrings(new CancellationTokenSource().Token))
                        {
                           // Console.WriteLine("we get a message : "+m.Text);//此处做了事件的分发处理。
                            await RouterFactory.operatingRoute(userConnector,m.Text);
                        }
                    }, TaskCreationOptions.LongRunning);
                   
            };

            // bind to the listen port across all interfaces
            await listener.StartListeningAsync(listenPort);
        }

        public static async Task sendCommandEntity(CommandEntity commandEntity, UserConnector userConnector) {

            await userConnector.WriteStringAsync(SerializeObject(commandEntity));
            await userConnector.client.WriteStream.FlushAsync();

        }
    }
}
