using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using static Newtonsoft.Json.JsonConvert;

namespace SocketOnlineServer
{
     /**
     * 这个类应该被修改为非阻塞的，它有可能很大的潜在性能问题。
     */
    public class RabbitListener
    {
        private RabbitListener() { }
        //单例模式防止创建多个对象
        //参数mssslist表示收到的消息列表，burable表示是否持久化，queueName表示队列名称无ack机制
        public static void RabbitmqReceive(ref List<string> messlist, bool burable, string queueName)
        {
            string Message = null;
            try
            {
                ConnectionFactory factory = RabbitHelper.GetFactory();
                using (IConnection conn = factory.CreateConnection())
                {
                    using (IModel channel = conn.CreateModel())
                    {
                        //在MQ上定义一个非持久化队列（第二个参数durable），如果名称相同不会重复创建
                        channel.QueueDeclare(queueName, burable, false, false, null);
                        //在队列上定义一个消费者
                        QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
                        //消费队列，并设置应答模式为程序主动应答
                        channel.BasicConsume(queueName, true, consumer);
                        while (true)
                        {
                            //这是一个阻塞的函数，会一直等待下一条消息
                            BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                            byte[] bytes = ea.Body;
                            Message = Encoding.UTF8.GetString(bytes);
                            messlist.Add(Message);
                            // Console.WriteLine("收到消息"+Message);

                        }
                    }
                }
            }
            catch (Exception e1)
            {
                Console.WriteLine(e1.ToString());
            }


        }
        //参数mssslist表示收到的消息列表，burable表示是否持久化，queueName表示队列名称，如果这个用户不在线则不操作消息，不进行ack然后把消息重新如队列。
        public static void RabbitmqReceive(ref List<UserMessage> me, bool burable, string queueName)
        {

            string Message = null;
            UserMessage userMessageReciced = null;
            try
            {
                ConnectionFactory factory = RabbitHelper.GetFactory();
                using (IConnection conn = factory.CreateConnection())
                {
                    using (IModel channel = conn.CreateModel())
                    {
                        //在MQ上定义一个非持久化队列（第二个参数durable），如果名称相同不会重复创建
                        channel.QueueDeclare(queueName, burable, false, false, null);
                        //在队列上定义一个消费者
                        QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
                        //消费队列，并设置应答模式为程序主动应答
                        channel.BasicConsume(queueName, false, consumer);
                        bool IsUserConnected = false;
                        while (true)
                        {
                            //这是一个阻塞的函数，会一直等待下一条消息
                            BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                            byte[] bytes = ea.Body;
                            Message = Encoding.UTF8.GetString(bytes);
                            userMessageReciced = DeserializeObject<UserMessage>(Message);
                            if (StorageStaticObject.UserSocketHashMap.ContainsKey(userMessageReciced.resiverId)) { IsUserConnected = true; }
                            //在此处处理message是否要保留。


                            if (IsUserConnected)
                            {
                                //在这里把消息发送到客户端。
                                me.Insert(0, userMessageReciced);
                                CommandEntity comm = new CommandEntity() {
                                    userId = userMessageReciced.senderId,
                                    userMessage = userMessageReciced,
                                    commandType = ICommandForMessage.ReciveMessage,
                                    commandUuid = Guid.NewGuid().ToString(),
                                    targetString = userMessageReciced.senderId
                                };
                                var task = Task.Run(async () => { await SocketServerConnector.sendCommandEntity(comm, StorageStaticObject.UserSocketHashMap[userMessageReciced.resiverId]); });
                                task.Wait();
                                Console.WriteLine("Send a message to"+ userMessageReciced.resiverId);
                                // MessageBox.Show("列表里添加了一条消息！");
                                channel.BasicAck(ea.DeliveryTag, false);
                                IsUserConnected = false;
                            }
                            else
                            {
                                Console.WriteLine("send back a message to rabbitmq.");
                                //返回没有收到消息
                                channel.BasicNack(ea.DeliveryTag, false,true);
                            }

                            // Console.WriteLine("收到消息"+Message);

                        }
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
