using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace SocketOnlineServer
{
    public class RedisProcesser
    {


        private static RedisClient redisClient =RedisFactory.RedisClient;

        private RedisProcesser(){

        }

        public static bool AddObjectByStringKey<T>(String key, T value, TimeSpan timeout)
        {
            bool flag = false;
            if (redisClient != null)
            {
                if (redisClient.ContainsKey(key)) {
                    redisClient.Del(key);
                }
                flag = redisClient.Add(key, value, timeout);
            }
            else
            {
                throw new ArgumentNullException("未初始化连接工厂。");
            }
            return flag;

        }


        public static T GetByStringKey<T>(String key)
        {
            T result = default(T);
            if (redisClient == null) {
                throw new ArgumentNullException("未初始化连接工厂。");
            }
            if (redisClient.ContainsKey(key))
            {
                result = redisClient.Get<T>(key);
            }

            return result;

        }


        public static bool AddHashByString(String hashId, String key, String value)
        {
            bool flag = false;
            if (redisClient == null)
            {
                throw new ArgumentNullException("未初始化连接工厂。");
            }
            flag = redisClient.SetEntryInHash(hashId, key, value);
            return flag;

        }

        public static List<String> GetHashKeysByHashId(String hashId)
        {
            List<String> keys = null;
            if (redisClient == null)
            {
                throw new ArgumentNullException("未初始化连接工厂。");
            }
            keys = redisClient.GetHashKeys(hashId);
            return keys;
        }



        public static List<String> GetHashValuesByHashId(String hashId)
        {
            List<String> values = null;
            if (redisClient == null)
            {
                throw new ArgumentNullException("未初始化连接工厂。");
            }
            values = redisClient.GetHashValues(hashId);
            return values;
        }

        public static List<String> GetAllRedisKeys()
        {
            List<String> keys = null;
            if (redisClient == null)
            {
                throw new ArgumentNullException("未初始化连接工厂。");
            }
            keys = redisClient.GetAllKeys();
            return keys;
        }

        /// <summary>
        /// 返回受影响的记录数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long DelectByKey(String key)
        {
            long count = 0;
            if (redisClient == null)
            {
                throw new ArgumentNullException("未初始化连接工厂。");
            }
            count = redisClient.Del(key);
            return count;
        }



        public static void AddToListByQueue(String key,String vlaue)
            {
            
            if (redisClient == null)
            {
                throw new ArgumentNullException("未初始化连接工厂。");
            }
            redisClient.EnqueueItemOnList(key,vlaue);
            
            //return flag;
            }


        /***
        private static void AddList(RedisClient client)
        {
            if (client == null) throw new ArgumentNullException("client");

            client.EnqueueItemOnList("QueueListId", "1.Learnghard");  //入队
            client.EnqueueItemOnList("QueueListId", "2.张三");
            client.EnqueueItemOnList("QueueListId", "3.李四");
            client.EnqueueItemOnList("QueueListId", "4.王五");
            var queueCount = client.GetListCount("QueueListId");

            for (var i = 0; i < queueCount; i++)
            {
                Console.WriteLine("QueueListId出队值：{0}", client.DequeueItemFromList("QueueListId"));   //出队(队列先进先出)
            }

            client.PushItemToList("StackListId", "1.Learninghard");  //入栈
            client.PushItemToList("StackListId", "2.张三");
            client.PushItemToList("StackListId", "3.李四");
            client.PushItemToList("StackListId", "4.王五");

            var stackCount = client.GetListCount("StackListId");
            for (var i = 0; i < stackCount; i++)
            {
                Console.WriteLine("StackListId出栈值：{0}", client.PopItemFromList("StackListId"));   //出栈(栈先进后出)
            }
        }

        //它是string类型的无序集合。set是通过hash table实现的，添加，删除和查找,对集合我们可以取并集，交集，差集
        private static void AddSet(RedisClient client)
        {
            if (client == null) throw new ArgumentNullException("client");

            client.AddItemToSet("Set1001", "A");
            client.AddItemToSet("Set1001", "B");
            client.AddItemToSet("Set1001", "C");
            client.AddItemToSet("Set1001", "D");
            var hastset1 = client.GetAllItemsFromSet("Set1001");
            foreach (var item in hastset1)
            {
                Console.WriteLine("Set无序集合Value:{0}", item); //出来的结果是无须的
            }

            client.AddItemToSet("Set1002", "K");
            client.AddItemToSet("Set1002", "C");
            client.AddItemToSet("Set1002", "A");
            client.AddItemToSet("Set1002", "J");
            var hastset2 = client.GetAllItemsFromSet("Set1002");
            foreach (var item in hastset2)
            {
                Console.WriteLine("Set无序集合ValueB:{0}", item); //出来的结果是无须的
            }

            var hashUnion = client.GetUnionFromSets(new string[] { "Set1001", "Set1002" });
            foreach (var item in hashUnion)
            {
                Console.WriteLine("求Set1001和Set1002的并集:{0}", item); //并集
            }

            var hashG = client.GetIntersectFromSets(new string[] { "Set1001", "Set1002" });
            foreach (var item in hashG)
            {
                Console.WriteLine("求Set1001和Set1002的交集:{0}", item);  //交集
            }

            var hashD = client.GetDifferencesFromSet("Set1001", new string[] { "Set1002" });  //[返回存在于第一个集合，但是不存在于其他集合的数据。差集]
            foreach (var item in hashD)
            {
                Console.WriteLine("求Set1001和Set1002的差集:{0}", item);  //差集
            }

        }

        /*
        sorted set 是set的一个升级版本，它在set的基础上增加了一个顺序的属性，这一属性在添加修改.元素的时候可以指定，
        * 每次指定后，zset(表示有序集合)会自动重新按新的值调整顺序。可以理解为有列的表，一列存 value,一列存顺序。操作中key理解为zset的名字.
        
        private static void AddSetSorted(RedisClient client)
        {
            if (client == null) throw new ArgumentNullException("client");

            client.AddItemToSortedSet("SetSorted1001", "A");
            client.AddItemToSortedSet("SetSorted1001", "B");
            client.AddItemToSortedSet("SetSorted1001", "C");
            var listSetSorted = client.GetAllItemsFromSortedSet("SetSorted1001");
            foreach (var item in listSetSorted)
            {
                Console.WriteLine("SetSorted有序集合{0}", item);
            }

            client.AddItemToSortedSet("SetSorted1002", "A", 400);
            client.AddItemToSortedSet("SetSorted1002", "D", 200);
            client.AddItemToSortedSet("SetSorted1002", "B", 300);

            // 升序获取第一个值:"D"
            var list = client.GetRangeFromSortedSet("SetSorted1002", 0, 0);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            //降序获取第一个值:"A"
            list = client.GetRangeFromSortedSetDesc("SetSorted1002", 0, 0);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }***/

    }
}
