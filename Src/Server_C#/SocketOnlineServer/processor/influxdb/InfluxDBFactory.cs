using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfluxDB.Net;
using InfluxDB.Net.Models;
using Newtonsoft.Json;
using static Newtonsoft.Json.JsonConvert;

namespace SocketOnlineServer
{
    public class InfluxDBFactory
    {
        private static InfluxDb client = new InfluxDb("http://139.199.35.140:8086", "Alban", "fj950602");
        private InfluxDBFactory()
        {
        }
        
        public static  async Task Should_Write_Typed_Rows_To_Database(UserMessageForRecord userChatRecord)
        {

            //client.CreateDatabaseAsync("MyQQ").Wait();
            // var infos = CreateTypedRowsStartingAt(new DateTime(2017,1,1,1,1,1,DateTimeKind.Utc),5);
            var infos = new Point();
            infos.Measurement = "ChartRecord";
            infos.Precision = InfluxDB.Net.Enums.TimeUnit.Seconds;
            //Tag is :  Start_senderId_TO_receiverId_End
            string jsonSerializedString = SerializeObject(userChatRecord);
            infos.Tags = new Dictionary<string, object> { { "record_name", "Start_"+userChatRecord.senderId+"_TO_"+userChatRecord.receiverId+"_End" }, };
            infos.Fields= new Dictionary<string, object> { { "value", jsonSerializedString }, };
            await client.WriteAsync("People_ChartRecord", infos);
            //client.QueryAsync;
        }

        public static async Task<List<UserMessageForRecord>> Should_Query_Typed_Rows_Form_Database(string senderId,string receiverId, int count)
        {
            string z = "Start_" + senderId + "_TO_" + receiverId + "_End";
            string f = "Start_" + receiverId + "_TO_" +senderId + "_End";
            string query = "select value from ChartRecord where record_name='"+z+ "' or record_name='"+f+ "' order by desc limit " + count+";";
            List<Serie> result= await client.QueryAsync("People_ChartRecord",query);
            List<UserMessageForRecord> userMessageList = new List<UserMessageForRecord>();
            foreach(Serie s in result){
                for (int i=0;i<s.Values.Length;i++) {
                    string t = (string)s.Values[i][1];
                    t = t.Replace("\\"," ");
                    UserMessageForRecord u = DeserializeObject<UserMessageForRecord>(t);
                    userMessageList.Add(u);
                }
            }
            return userMessageList;
        }
        /*
         RedisFactory.createRedisClient("139.199.35.140", "6379", "fj950602", "0");
            RedisProcesser.AddObjectByStringKey<String>("test", "Learninghard,very cool!", new TimeSpan(0,0,0,50));
            Debug.Assert(RedisProcesser.GetByStringKey<String>("test").Equals("Learninghard,very cool!"));
         */

    }
}
