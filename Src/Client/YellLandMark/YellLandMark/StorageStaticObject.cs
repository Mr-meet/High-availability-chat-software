using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellLandMark.pages;

namespace YellLandMark
{
    public class StorageStaticObject
    {
        private StorageStaticObject() { }
        /// <summary>
        /// 
        /// </summary>
        public static string userNumber { get; set; }
        public static List<DictionaryInfo> dictionaryInfoList { get; set; }
        public static Dictionary<string, CommandEntity> commandQueue = new Dictionary<string, CommandEntity>();
        public static Dictionary<string, UserInfo> newerUserInfomation = new Dictionary<string, UserInfo>();

        public static Dictionary<string, List<UserMessage>> LocalMessageQueue = new Dictionary<string, List<UserMessage>>();

        
        public static List<Friend> localFriendsList { get; set; }
        
        public static Dictionary<int, GroupInfo> localGroupMap { get; set; }

        /// <summary>
        /// page single object
        /// </summary>
        public static Loading loadingPage { get; set; }
        public static Register registerPage { get; set; }
        public static PageBlank pageBlank { get; set; }
       // public static ChatPage chatPage { get; set; }
    }
}
