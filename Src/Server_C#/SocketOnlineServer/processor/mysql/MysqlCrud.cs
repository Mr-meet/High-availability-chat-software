using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketOnlineServer
{
    public class MysqlCrud
    {
        private MysqlCrud() {}
        /// <summary>
        /// 数据库校验，返回登陆成功或者失败，匹配成功则返回true并且更新上次上线时间，否则返回false。
        /// </summary>
        /// <param name="usernum"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool userLogin(string usernum,string password)
        {
            string loginSqlCommand = "select last_login_time from user_login_info where user_num='"+usernum+"' and password='"+password+"';" ;
            //DataSet result = MySqlHelper.Query(loginSqlCommand);
            MySqlDataReader result = MySqlHelper.QueryObject(loginSqlCommand);
            if (result.Read())
            {
                result.Close();
                DateTime dt = DateTime.Now;
                string time_now = dt.ToString("yyyy-MM-dd HH:mm:ss");
                string updateLoginTimeCommand = "UPDATE user_login_info SET last_login_time = '" + time_now + "' WHERE user_num = '" + usernum + "';";
                MySqlHelper.ExecuteSql(updateLoginTimeCommand);
                return true;
            }
            else {
                result.Close();
                return false;
            }
          
            
           
        }

        /// <summary>
        /// 每一位用户注册都需要操作三张表，只有三张表都成功那么用户才能被添加。
        /// （这里的图片需要斟酌，而且需要事务支持！）//TODO
        /// </summary>
        /// <param name="userinfo"></param>
        /// <param name="userNum"></param>
        /// <returns></returns>
        public static UserLoginInfo userRegiste(UserInfo userInfo,string password)
        {
            int successCounter = 0;
            string userNum = null;
            string userNumTemp = "";
            UserLoginInfo userLoginInfo = new UserLoginInfo();
            userLoginInfo.user_num = userNum;
            Random r = new Random();
            while (userNum==null) {
                for (int i = 0; i < 9; i++) {
                    userNumTemp+=r.Next(9);
                }
                if (!existUser(userNumTemp)) {
                    userNum = userNumTemp;
                    userLoginInfo.user_num = userNum;
                }
            }
            DateTime dt = DateTime.Now;
            string time_now = dt.ToString("yyyy-MM-dd HH:mm:ss");
            string userLoginInfoInsert = "INSERT INTO user_login_info (user_num" +
                ",password,last_login_time,regist_time) VALUES ('"+ userNum + "','"+ password + "','"+ time_now + "','"+ time_now + "');";

            string userInfoInsertSql = "INSERT INTO user_info (user_name,user_num,signature_of_personality,homeland,birthday,email,career,place_of_residence,gender,image,homepage,icon)" +
                " VALUES ('"+userInfo.user_name+"','"+userNum+"','"+ userInfo .signature_of_personality+ "','"+ userInfo.homeland+ "','"+ userInfo.birthday.ToString("yyyy-MM-dd")+ "','"+ userInfo.email+ "','"+ userInfo .career+ "','"+ userInfo.place_of_residence+ "','"+ userInfo.gender+ "','"+ userInfo.image+ "','"+ userInfo.homepage+ "','"+userInfo.icon+"');";
            string userVipInfoInsertSql = "INSERT INTO user_vip_level (user_num,vip_flag,use_time_counter,vip_time_counter,day_counter) VALUES ('"+userNum+"',"+0+ "," + 0 + "," + 0 + "," + 0 + ");";
            if (MySqlHelper.ExecuteSql(userLoginInfoInsert) > 0) successCounter++;
            if (MySqlHelper.ExecuteSql(userInfoInsertSql) > 0) successCounter++;
            if (MySqlHelper.ExecuteSql(userVipInfoInsertSql) > 0) successCounter++;
            if (successCounter == 3) {
                Console.WriteLine("Add a new user success! userId is "+ userNum+"\n");
                return userLoginInfo;
            }
            
            return userLoginInfo;
        }

        /// <summary>
        /// 判断用户是否存在，存在则返回true，否则返回false。
        /// </summary>
        /// <param name="userNum"></param>
        /// <returns></returns>
        public static bool existUser(string userNum)
        {
            string loginSqlCommand = "select * from user_login_info where user_num='" + userNum +"';";
            MySqlDataReader result = MySqlHelper.QueryObject(loginSqlCommand);
            if (result.Read())
            {
                result.Close();
                return true;
            }
            else
            {
                result.Close();
                return false;
            }
        }

        /// <summary>
        /// 返回所有useage字段符合[career][gender][place][platform][wrongType][ServerMessage] 的字典对象的集合
        /// </summary>
        /// <returns></returns>
        public static List<DictionaryInfo> findDictionaryInfo() {
            string SqlCommand = "select * from dictionary where useage='career' OR useage='gender'  OR useage='place'  OR useage='platform'  OR useage='wrongType'  OR useage='ServerMessage';";
            MySqlDataReader result = MySqlHelper.QueryObject(SqlCommand);
            List<DictionaryInfo> dictionInfoList = new List<DictionaryInfo>();

            while (result.Read()) {
                DictionaryInfo tempDictionary = new DictionaryInfo();
                tempDictionary.uuid = result["uuid"].ToString();
                tempDictionary.value = result["value"].ToString();
                tempDictionary.useage = result["useage"].ToString();
                tempDictionary.name = result["name"].ToString();
                tempDictionary.demo = result["demo"].ToString();
                dictionInfoList.Add(tempDictionary);
            }
            result.Close();
            return dictionInfoList;
        }

        /// <summary>
        /// 根据用户ID查找用户信息
        /// </summary>
        /// <param name="userNum"></param>
        /// <returns></returns>
        public static UserInfo findUserInfomation(string userNum) {
            string SqlCommand = "select * from user_info where user_num='" + userNum + "';";
            MySqlDataReader result = MySqlHelper.QueryObject(SqlCommand);
            UserInfo userInfo=null;

            if (result.Read())
            {
                userInfo = new UserInfo();
                userInfo.user_name = result["user_name"].ToString();
                userInfo.user_num = result["user_num"].ToString();
                userInfo.career = result["career"].ToString();
                userInfo.email = result["email"].ToString();
                userInfo.gender = result["gender"].ToString();
                userInfo.homeland = result["homeland"].ToString();
                userInfo.homepage = result["homepage"].ToString();
                userInfo.place_of_residence = result["place_of_residence"].ToString();
                userInfo.signature_of_personality = result["signature_of_personality"].ToString();
                userInfo.birthday = (DateTime)result["birthday"];
                userInfo.icon = (byte[])result["icon"];
                userInfo.image = (byte[])result["image"];

            }
            result.Close();
            return userInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="friendLike"></param>
        /// <returns></returns>
        public static List<UserInfo> findFriends(string friendLike)
        {

            return null;
        }


        public static bool insertCommunity() { return true; }
        public static bool findCommunity() { return true; }

        public static bool updateUserInfomation() { return true; }
        public static bool insertDebugInfo() { return true; }

    }
}
