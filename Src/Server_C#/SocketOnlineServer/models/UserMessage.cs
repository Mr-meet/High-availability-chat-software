using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


    public class UserMessage
    {
        //消息ID
        [JsonProperty("id")]
        public string id { get; set; }
        //發送者的
        [JsonProperty("senderId")]
        public string senderId { get; set; }
        //接收者的ID
        [JsonProperty("resiverId")]
        public string resiverId { get; set; }
        //消息發送的時間
        [JsonProperty("sendTime")]
        public string sendTime { get; set; }
        //用戶所使用的平台
        [JsonProperty("platform")]
        public int platform { get; set; }
        [JsonProperty("contentChecked")]
        public bool contentChecked { get; set; }
        [JsonProperty("resiverPlatform")]
        public int resiverPlatform { get; set; }
        [JsonProperty("content")]
        public string content { get; set; }
        [JsonProperty("photoContent")]
        public byte[] photoContent { get; set; }
        [JsonProperty("attachedMessage")]
        public string attachedMessage { get; set; }
    }

