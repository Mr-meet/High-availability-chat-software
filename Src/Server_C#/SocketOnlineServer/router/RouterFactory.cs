using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using static Newtonsoft.Json.JsonConvert;

namespace SocketOnlineServer
{
    /// <summary>
    /// 做消息和处理的分发类
    /// </summary>
    public class RouterFactory
    {
        private RouterFactory() { }
        private static bool IsNeedBack = false;
        public static async Task operatingRoute(UserConnector userConnector, string commandString) {
            IsNeedBack = false;
            CommandEntity commandEntity = DeserializeObject<CommandEntity>(commandString);
            //当用户登陆之后而且还未被登记进维护对象中时，进行添加维护对象，为了以后推送消息方便。
            if (!commandEntity.userId.Equals("未登录用户")) {
                //这里做多平台在线的操作

                if (!StorageStaticObject.UserSocketHashMap.ContainsKey(commandEntity.userId)) {
                    StorageStaticObject.UserSocketHashMap.Add(commandEntity.userId, userConnector);
                    Console.WriteLine( commandEntity.userId+"  has been listion!");
                }
                else
                {
                    //这里本来应该用来做多平台连接。
                    StorageStaticObject.UserSocketHashMap.Remove(commandEntity.userId);
                    StorageStaticObject.UserSocketHashMap.Add(commandEntity.userId, userConnector);
                }
                
            }

            CommandEntity commandEntityForback = new CommandEntity
            {
                commandType = ICommandForMessage.ReciveMessage,
                commandUuid = commandEntity.commandUuid,
                userId = commandEntity.userId
            };



            switch (commandEntity.commandType)
            {
                case ICommandForMessage.Hello :
                    Console.WriteLine("A new client say hello!");
                    break;
                case ICommandForMessage.LoadDictionary:
                    Console.WriteLine("A  client want LoadDictionary!");
                    commandEntityForback = LoadDictionary(commandEntityForback);
                    //commandTempContentType = "List<DictionaryInfo>";
                    IsNeedBack = true;
                    break;
                case ICommandForMessage.Login:
                    Console.WriteLine("A  client want Login!");
                    commandEntityForback = Login(commandEntityForback,commandEntity);
                    IsNeedBack = true;
                    Console.WriteLine("Login deal ok!");
                    break;
                case ICommandForMessage.Registe:
                    Console.WriteLine("A new registe request!");
                    commandEntityForback = Registe(commandEntityForback,commandEntity);
                    IsNeedBack = true;
                    break;
                case ICommandForMessage.AddFriend:
                    Console.WriteLine("a user want add a friend");
                    commandEntityForback=addFriend(commandEntityForback, commandEntity);
                    IsNeedBack = true;
                    //Add friend
                    break;
                case ICommandForMessage.FindFriend:
                    Console.WriteLine("a user want add a group");
                    //add afriends
                    break;
                case ICommandForMessage.AddGroup:

                    IsNeedBack = true;
                    break;
                case ICommandForMessage.SendMessage:
                    Console.WriteLine(commandEntity.userId+" send a message!");
                    SendMessage(commandEntityForback,commandEntity);
                    IsNeedBack = false;
                    break;
                case ICommandForMessage.GetMessageRecord:
                    commandEntityForback = getMessageRecord(commandEntityForback, commandEntity);
                    IsNeedBack = true;
                    //get influxDB
                    break;
                default : Console.WriteLine("Find a unknow command!----"+commandEntity.commandType+"\n");
                    break;
            }



            if (IsNeedBack)
            {
                //commandEntityForback.content = commandTempContent,
                //commandEntityForback.contentType = commandTempContentType,
                //commandEntityForback.dictionaryInfoList = (List<DictionaryInfo>)commandTempContent,
                Console.WriteLine("Send back a message!"+commandEntityForback.ToString());
                await userConnector.WriteStringAsync(SerializeObject(commandEntityForback));
                await userConnector.client.WriteStream.FlushAsync();
                IsNeedBack = false;
            }
            
        }

        private static void SendMessage(CommandEntity comm, CommandEntity clientCommand) {
            UserMessage u = clientCommand.userMessage;
            RabbitProductor.RabbitmqSend(SerializeObject(u), true, RabbitHelper.sendQueueName);
            UserMessageForRecord record = new UserMessageForRecord() {
                senderId = u.senderId,
                messageSendTime=u.sendTime,
                message=u.content,
                receiverId=u.resiverId,
                photo=u.photoContent
            };
            Task.Factory.StartNew(()=> InfluxDBFactory.Should_Write_Typed_Rows_To_Database(record)); ;
        }

        private static CommandEntity LoadDictionary(CommandEntity comm) {
            comm.dictionaryInfoList = MysqlCrud.findDictionaryInfo();
            return comm;
        }



        private static CommandEntity Login(CommandEntity comm, CommandEntity clientCommand)
        {
            comm.isSuccess = MysqlCrud.userLogin(clientCommand.userLoginInfo.user_num, clientCommand.userLoginInfo.password);
            comm.groupMap = RedisProcesser.GetByStringKey<Dictionary<int, GroupInfo>>("userGroup_" + clientCommand.userLoginInfo.user_num);
            comm.friendsList = RedisProcesser.GetByStringKey<List<Friend>>(clientCommand.userLoginInfo.user_num);
            comm.userInfo = MysqlCrud.findUserInfomation(clientCommand.userLoginInfo.user_num);
            comm.dictionaryInfoList = MysqlCrud.findDictionaryInfo();
            //此处要进行踢下线操作！
            return comm;
        }



        private static CommandEntity Registe(CommandEntity comm, CommandEntity clientCommand) {

            comm.userLoginInfo = MysqlCrud.userRegiste(clientCommand.userInfo, clientCommand.userLoginInfo.password);

            Dictionary<int, GroupInfo> groupInfos = new Dictionary<int, global::GroupInfo>();
            
            groupInfos.Add(-1, new GroupInfo() {groupId=-1,groupName= "黑名单" });
            groupInfos.Add(0, new GroupInfo() { groupId = 0, groupName = "陌生人" });
            groupInfos.Add(1, new GroupInfo() { groupId = 1, groupName = "我的好友" });
            groupInfos.Add(2, new GroupInfo() { groupId = 2, groupName = "特别关心" });
            string groupKeyForRedis = "userGroup_"+comm.userLoginInfo.user_num;
            List<Friend> friends = new List<Friend>();
            friends.Add(new Friend() {
                id = comm.userLoginInfo.user_num,
                group = 1,
                remark=clientCommand.userInfo.user_name,
                friendShip=0
            });
            string friendsKeyForRedis = comm.userLoginInfo.user_num;

            RedisProcesser.AddObjectByStringKey<Dictionary<int, GroupInfo>>(groupKeyForRedis, groupInfos, new TimeSpan(30, 0, 0, 0));
            RedisProcesser.AddObjectByStringKey<List<Friend>>(friendsKeyForRedis,friends,new TimeSpan(30, 0, 0, 0));
            return comm;
        }



        private static CommandEntity addFriend(CommandEntity comm, CommandEntity clientCommand) {

            if (MysqlCrud.existUser(clientCommand.targetString)) {
                
                string friendsKeyForRedis = comm.userId;
                List<Friend> friends=  RedisProcesser.GetByStringKey<List<Friend>>(friendsKeyForRedis);
                friends.Add(new Friend() {

                    id = clientCommand.targetString,
                    group = 1,
                    friendShip = 0,
                    remark=MysqlCrud.findUserInfomation(clientCommand.targetString).user_name,
                    
                });
                comm.friendsList = friends;
                RedisProcesser.AddObjectByStringKey<List<Friend>>(friendsKeyForRedis, friends, new TimeSpan(30, 0, 0, 0));
                friends = RedisProcesser.GetByStringKey<List<Friend>>(clientCommand.targetString);
                friends.Add(new Friend()
                {

                    id = clientCommand.userId,
                    group = 1,
                    friendShip = 0,
                    remark = MysqlCrud.findUserInfomation(clientCommand.userId).user_name,

                });
                RedisProcesser.AddObjectByStringKey<List<Friend>>(clientCommand.targetString, friends, new TimeSpan(30, 0, 0, 0));
                comm.isSuccess = true;
                return comm;
            }
            else
            {
                comm.isSuccess = false;
                return comm;
            }
        }


        private static CommandEntity getMessageRecord(CommandEntity comm, CommandEntity clientCommand)
        {
            List<UserMessageForRecord> temp=null;
            var result = Task.Run(async () => { temp = await InfluxDBFactory.Should_Query_Typed_Rows_Form_Database(clientCommand.userId, clientCommand.targetString, 10); });
            result.Wait();
            comm.userRecord =temp;
            return comm;
        }








    }
}
