using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using System.Threading;

namespace SocketOnlineServer
{
    public class RedisFactory
    {
        private static RedisClient redisClient=null;
        private RedisFactory() {
            
        }
        private static string port = "6379";
        private static string ip = "139.199.35.140";
        private static string passWord = "fj950602";
        private static string dbIndex = "0";

        /**
         
               RedisFactory.createRedisClient("139.199.35.140", "6379", "fj950602", "0");
            RedisProcesser.AddObjectByStringKey<String>("test", "Learninghard,very cool!", new TimeSpan(0,0,0,50));
            Debug.Assert(RedisProcesser.GetByStringKey<String>("test").Equals("Learninghard,very cool!"));
            
             */

        /// <summary>
        /// 初始化redis的客户端连接
        /// </summary>
        /// <param name="ip">redis数据库服务器ip</param>
        /// <param name="port">端口</param>
        /// <param name="passWord">数据库密码</param>
        /// <param name="dbIndex">数据库序号索引</param>
        /// <remarks>传入参数全为string即可，无则为null</remarks>
        public static void  createRedisClient(String ip,String port,String passWord,String dbIndex)
        {
            if (redisClient == null)
            {
                String realPassWord = null;
                long realDbIndex = 0;
                int realPort ;
                if (!StringHelper.isNullOrEmpty(ip)&& !StringHelper.isNullOrEmpty(port)) {
                    realPort = int.Parse(port);
                    if (!StringHelper.isNullOrEmpty(passWord))
                    {
                        realPassWord = passWord;
                    }
                    if (!StringHelper.isNullOrEmpty(dbIndex))
                    {
                        realDbIndex = long.Parse(dbIndex);
                    }
                    redisClient = new RedisClient(ip, realPort, realPassWord, realDbIndex);
                }
              
            }
            
        }


        public static RedisClient RedisClient
        {

            get {
                if (redisClient == null)
                {
                    createRedisClient(ip,port,passWord,dbIndex);
                }
                return redisClient;
            }
        }

        //public static string getString()
        //{
        //    var timeOut = new TimeSpan(0, 0, 0, 10);
        //    redisClient.Add("Test", "Learninghard", timeOut);
        //    if (redisClient.ContainsKey("Test"))
        //    {
        //        return redisClient.Get<string>("Test");   
        //    }
        //    return null;
        //}

    }
}
