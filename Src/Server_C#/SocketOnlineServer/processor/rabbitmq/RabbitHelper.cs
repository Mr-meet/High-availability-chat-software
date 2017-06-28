using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace SocketOnlineServer
{
    public class RabbitHelper
    {
        public static string sendQueueName = "USER_SEND_QUEUE";
        public static string reciveQueueName = "USER_RESAVE_QUEUE";
        public static string exChange = "QUEUE_DIRECT";
        private static string myHost = "139.199.35.140";//服务器IP
        private static string myName = "Alban";//rabbitmq账户名
        private static string myPasswd = "fj950602";//rabbitmq密码
        private static int myPort = 5672;//rabbitmq端口号
        private static ConnectionFactory factory = null;
        private RabbitHelper() { }//单例模式，防止被创建对象 
       
        public static ConnectionFactory GetFactory()
        {
            if (factory == null)
            {
                factory = new ConnectionFactory();
                factory.HostName = myHost;
                factory.Port = myPort;
                factory.UserName = myName;
                factory.Password = myPasswd;
                return factory;
            }
            else
            {
                return factory;
            }
        }
    }
}
