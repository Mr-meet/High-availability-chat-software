using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketOnlineServer
{
    class Program
    {
       

        static void Main(string[] args)
        {
            //Console.WriteLine("Program start! Time:" + DateTime.Now.ToString());
            MySqlHelper.openConnection();
            var task = Task.Run(async () => { await SocketServerConnector.openLinsten(); });
            task.Wait();
            //Console.WriteLine("Socket linstener is already start! Time:" + DateTime.Now.ToString());
            
            var temp = new List<UserMessage>();
            RabbitListener.RabbitmqReceive(ref temp,true,RabbitHelper.reciveQueueName);

            //UserMessageForRecord userRecord = new UserMessageForRecord() {
            //    message = "This is a first test message!",
            //    receiverId = "Cyan",
            //    senderId = "Alban",
            //    messageSendTime = DateTime.Now.ToString(),
            //};
            //List<UserMessageForRecord> temp;
            //var task = Task.Run(async () => { await InfluxDBFactory.Should_Write_Typed_Rows_To_Database(userRecord); });
            //task.Wait();
            //var result = Task.Run(async () => { temp=await InfluxDBFactory.Should_Query_Typed_Rows_Form_Database("Alban","Cyan", 10); });
            //result.Wait();


        }

    }
}
