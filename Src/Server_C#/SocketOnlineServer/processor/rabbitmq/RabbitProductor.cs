using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace SocketOnlineServer
{
    public class RabbitProductor
    {

        private RabbitProductor()
        {//单例模式防止他人创建实例
        }
        //第一个参数是要发送的消息，第二个参数表明是否要持久化
        public static void RabbitmqSend(string outmessage, bool burable, string queueName)
        {
            string meessage = outmessage;
            try
            {
                ConnectionFactory factory = RabbitHelper.GetFactory();
                using (IConnection conn = factory.CreateConnection())
                {
                    using (IModel channel = conn.CreateModel())
                    {
                        //在MQ上定义一个非持久化队列（第二个参数durable），如果名称相同不会重复创建
                        channel.QueueDeclare(queueName, burable, false, false, null);
                        byte[] bytes = Encoding.UTF8.GetBytes(meessage);
                        channel.BasicPublish(RabbitHelper.exChange, queueName, null, bytes);
                        //channel.BasicPublish(RabbitHelper.exChange, queueName, null, bytes);
                        
                        Console.WriteLine("发出消息"+meessage);
                        //MessageBox.Show("发出消息" + meessage);
                    }
                }
            }
            catch (Exception e1)
            {
                Console.WriteLine(e1.ToString());
            }
        }


    }
}
