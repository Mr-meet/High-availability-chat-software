using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class CommandEntity
{
    [JsonProperty("commandType")]
    public string commandType { get; set; }
    [JsonProperty("commandUuid")]
    public string commandUuid { get; set; }
    [JsonProperty("userId")]
    public string userId { get; set; }
    [JsonProperty("contentType")]
    public string contentType { get; set; }
    [JsonProperty("content")]
    public Object content{get;set;}






    [JsonProperty("dictionaryInfoList")]
    public List<DictionaryInfo> dictionaryInfoList { get; set; }
    [JsonProperty("isSuccess")]
    public bool isSuccess { get; set; }
    [JsonProperty("userInfo")]
    public UserInfo userInfo { get; set; }
    [JsonProperty("userLoginInfo")]
    public UserLoginInfo userLoginInfo { get; set; }


    [JsonProperty("searchString")]
    public string searchString { get; set; }
    [JsonProperty("userInfoList")]
    public List<UserInfo> userInfoList { get; set; }
    [JsonProperty("friendsList")]
    public List<Friend> friendsList { get; set; }
    [JsonProperty("groupMap")]
    public Dictionary<int, GroupInfo> groupMap { get; set; }
    [JsonProperty("targetString")]
    public string targetString { get; set; }

    [JsonProperty("userMessage")]
    public UserMessage userMessage { get; set;}
    [JsonProperty("userRecord")]
    public List<UserMessageForRecord> userRecord { get; set; }

}
