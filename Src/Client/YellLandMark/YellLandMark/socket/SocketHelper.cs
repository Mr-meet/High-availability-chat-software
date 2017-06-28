using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;
using Sockets;
using System.IO;
using System.Threading;
using static Newtonsoft.Json.JsonConvert;

namespace YellLandMark
{
    public class SocketHelper
    {
       public static UserConnector userConnector;
        public static CancellationTokenSource tonkenSource;
        public static bool IsStopReadMessage = false;
        private SocketHelper() { }
        public static async Task connectToService()
        {
             var address = "172.16.80.1";
           // var address = "192.168.42.162";//
            var port = 11000;
            var client = new TcpSocketClient();
            await client.ConnectAsync(address, port);
            userConnector = new UserConnector(client);
            tonkenSource = new CancellationTokenSource();
            var cancellationTokenSource = new CancellationTokenSource();
           // var threadCancellationToken = new CancellationToken();
            // we're connected!
            await sayHello("未登录用户");
            //await Task.Factory.StartNew(() =>
            //{
            //    foreach (var m in userConnector.ReadStrings(tonkenSource.Token))
            //    {
            //        CommandEntity commandEntityForSave = DeserializeObject<CommandEntity>(m.Text);
            //        //操作的回执则将commandEntity放入map
            //        StorageStaticObject.commandQueue.Add(commandEntityForSave.commandUuid, commandEntityForSave);
            //    }
            //}, TaskCreationOptions.LongRunning);
           
            

            //  await client.DisconnectAsync();
        }
        public static async Task sayHello(string userID) {
            CommandEntity commandEntity = new CommandEntity()
            {
                commandType = ICommandForMessage.Hello,
                content = null,
                contentType = null,
                commandUuid = Guid.NewGuid().ToString(),
                userId = userID
            };
            await userConnector.WriteStringAsync(SerializeObject(commandEntity));
            await userConnector.client.WriteStream.FlushAsync();
        }

        public static async Task sendCommandEntity(CommandEntity commandEntity) {
            await userConnector.WriteStringAsync(SerializeObject(commandEntity));
            await userConnector.client.WriteStream.FlushAsync();
        }

        public static void saveDictionaryList(string commandUuid){
            if (StorageStaticObject.commandQueue.ContainsKey(commandUuid))
            {
                StorageStaticObject.dictionaryInfoList = (List<DictionaryInfo>)StorageStaticObject.commandQueue[commandUuid].content;
                StorageStaticObject.commandQueue.Remove(commandUuid);
            }
            else
            {
                CommandEntity commandEntity = getCommandEntityByUuid(commandUuid);
                if (commandEntity != null)
                {
                    // StorageStaticObject.dictionaryInfoList = (List<DictionaryInfo>)commandEntity.content;
                    StorageStaticObject.dictionaryInfoList = commandEntity.dictionaryInfoList;
                }
                else
                {
                    //出错了。
                }
            }
           
        }

        public static UserLoginInfo registeBack(string commandUuid) {
            UserLoginInfo user = new UserLoginInfo();
            user.user_num = null;
            if (StorageStaticObject.commandQueue.ContainsKey(commandUuid))
            {
                user = StorageStaticObject.commandQueue[commandUuid].userLoginInfo;
                StorageStaticObject.commandQueue.Remove(commandUuid);
                return user;
            }
            else
            {
                CommandEntity commandEntity = getCommandEntityByUuid(commandUuid);
                if (commandEntity != null)
                {
                    user = commandEntity.userLoginInfo;
                    return user;
                }
                else
                {
                    return user;//出错了。
                }
            }
        }

        public static bool opertionIsSuccess(string commandUuid)
        {
            bool isSuccess = false;
            if (StorageStaticObject.commandQueue.ContainsKey(commandUuid))
            {
                 isSuccess = StorageStaticObject.commandQueue[commandUuid].isSuccess;
                StorageStaticObject.commandQueue.Remove(commandUuid);
                return isSuccess;
            }
            else
            {
                CommandEntity commandEntity = getCommandEntityByUuid(commandUuid);
                if (commandEntity != null)
                {
                    isSuccess = commandEntity.isSuccess;
                    return isSuccess;
                }
                else
                {
                    return isSuccess;//出错了。
                }
            }
        }


        public static bool loginOpertion(string commandUuid) {
            bool isSuccess = false;
            if (StorageStaticObject.commandQueue.ContainsKey(commandUuid))
            {
                isSuccess = StorageStaticObject.commandQueue[commandUuid].isSuccess;
                StorageStaticObject.commandQueue.Remove(commandUuid);
                return isSuccess;
            }
            else
            {
                CommandEntity commandEntity = getCommandEntityByUuid(commandUuid);
                if (commandEntity != null)
                {
                    isSuccess = commandEntity.isSuccess;
                    StorageStaticObject.localFriendsList = commandEntity.friendsList;
                    StorageStaticObject.localGroupMap = commandEntity.groupMap;
                    if (commandEntity.userInfo != null) {
                        StorageStaticObject.newerUserInfomation.Add(commandEntity.userInfo.user_num, commandEntity.userInfo);
                    }
                    StorageStaticObject.dictionaryInfoList = commandEntity.dictionaryInfoList;

                    return isSuccess;
                }
                else
                {
                    return isSuccess;//出错了。
                }
            }
        }

        public static List<UserMessageForRecord> getRecordMessage(string commandUuid) {
            List<UserMessageForRecord> messageRecords = null;
            if (StorageStaticObject.commandQueue.ContainsKey(commandUuid))
            {
                messageRecords = StorageStaticObject.commandQueue[commandUuid].userRecord;
                StorageStaticObject.commandQueue.Remove(commandUuid);
                return messageRecords;
            }
            else
            {
                CommandEntity commandEntity = getCommandEntityByUuid(commandUuid);
                if (commandEntity != null)
                {
                    messageRecords = commandEntity.userRecord;


                    return messageRecords;
                }
                else
                {
                    return messageRecords;//出错了。
                }
            }
        }


        public static CommandEntity getCommandEntityByUuid(string commandUuid) {
            foreach (var m in userConnector.ReadStrings(tonkenSource.Token))
            {
                CommandEntity commandEntity = DeserializeObject<CommandEntity>(m.Text);
                if (commandEntity.commandUuid.Equals(commandUuid))
                {
                    return commandEntity;
                }
                else
                {//不是此操作的回执则将commandEntity放入map
                    StorageStaticObject.commandQueue.Add(commandEntity.commandUuid, commandEntity);
                }
            }
            return null;
        }
        public static void readMessage() {

            List<UserMessage> userMessageList;

            foreach (var m in userConnector.ReadStrings(tonkenSource.Token))
            {
                CommandEntity commandEntity = DeserializeObject<CommandEntity>(m.Text);
                if (commandEntity.commandType.Equals(ICommandForMessage.ReciveMessage))
                {
                    if (StorageStaticObject.LocalMessageQueue.ContainsKey(commandEntity.targetString))
                    {
                        userMessageList = StorageStaticObject.LocalMessageQueue[commandEntity.targetString];
                    }
                    else {
                        userMessageList = new List<UserMessage>();
                        userMessageList.Add(commandEntity.userMessage);
                    }
                    if (StorageStaticObject.LocalMessageQueue.ContainsKey(commandEntity.targetString))
                    {
                        userMessageList.AddRange(StorageStaticObject.LocalMessageQueue[commandEntity.targetString]);
                        StorageStaticObject.LocalMessageQueue.Remove(commandEntity.targetString);
                    }
                    StorageStaticObject.LocalMessageQueue.Add(commandEntity.targetString, userMessageList);
                    
                    
                    if (IsStopReadMessage) { return; }
                }
                else
                {//不是此操作的回执则将commandEntity放入map
                    StorageStaticObject.commandQueue.Add(commandEntity.commandUuid, commandEntity);
                    if (IsStopReadMessage) { return; }
                }
            }
            return ;

        }

     /*   public static CommandEntity getHay() {
             
            foreach (var m in userConnector.ReadStrings(tonkenSource.Token))
            {
                CommandEntity commandEntity = DeserializeObject<CommandEntity>(m.Text);
                if (commandEntity.commandType.Equals(ICommandForMessage.Hey))
                {
                    return commandEntity;
                }
                else
                {//不是此操作的回执则将commandEntity放入map
                    StorageStaticObject.commandQueue.Add(commandEntity.commandUuid, commandEntity);
                }
                
            }
            return null;
        }*/
    }
}
