using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


    /**
     Key 格式为 Start_senderId_TO_receiverId_End.
其中存的值即为聊天记录。是一个自定义类型：UserMessageForRecord。
查询时分两次，获得部分聊天记录。聊天记录做30*天持久化操作。非会员做7*天持久化操作。

         */
    public class UserMessageForRecord
    {
        [JsonProperty("senderId")]
        public string senderId { get; set; }
        [JsonProperty("receiverId")]
        public string receiverId { get; set; }
        [JsonProperty("messageSendTime")]
        public string messageSendTime { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("photo")]
        public byte[] photo { get; set; }
    }

