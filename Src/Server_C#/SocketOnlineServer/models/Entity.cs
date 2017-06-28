using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Entity
    {
     
    }

public class BannedInfo {
    [JsonProperty("disable")]
    public bool disable { get; set; }
    [JsonProperty("uuid")]
    public string uuid { get; set; }
    [JsonProperty("user_num")]
    public string user_num { get; set; }
    [JsonProperty("time_line")]
    public DateTime time_line { get; set; }
    [JsonProperty("time_long")]
    public long time_long { get; set; }
}

public class CommunityInfo {
    [JsonProperty("community_id")]
    public string community_id { get; set; }
    [JsonProperty("community_name")]
    public string community_name { get; set; }
    [JsonProperty("community_type")]
    public string community_type { get; set; }
    [JsonProperty("community_user_max_number")]
    public string community_user_max_number { get; set; }
    [JsonProperty("admin_account")]
    public string admin_account { get; set; }
    [JsonProperty("icon")]
    public byte[] icon { get; set; }
    [JsonProperty("other_info")]
    public string other_info { get; set; }
}


public class DictionaryInfo {
    [JsonProperty("uuid")]
    public string uuid { get; set; }
    [JsonProperty("value")]
    public string value { get; set; }
    [JsonProperty("name")]
    public string name { get; set; }
    [JsonProperty("useage")]
    public string useage { get; set; }
    [JsonProperty("demo")]
    public string demo { get; set; }
}


public class ProgramDebug {
    [JsonProperty("uuid")]
    public string uuid { get; set; }
    [JsonProperty("platform")]
    public string platform { get; set; }
    [JsonProperty("time")]
    public  DateTime time { get; set; }
    [JsonProperty("type")]
    public string type { get; set; }
    [JsonProperty("message")]
    public string message { get; set; }
}


public class UserInfo {
    [JsonProperty("user_name")]
    public string user_name { get; set; }
    [JsonProperty("user_num")]
    public string user_num { get; set; }
    [JsonProperty("signature_of_personality")]
    public string signature_of_personality { get; set; }
    [JsonProperty("homeland")]
    public string homeland { get; set; }
    [JsonProperty("birthday")]
    public DateTime birthday { get; set; }
    [JsonProperty("email")]
    public string email { get; set; }
    [JsonProperty("career")]
    public string career { get; set; }
    [JsonProperty("place_of_residence")]
    public string place_of_residence { get; set; }
    [JsonProperty("gender")]
    public string gender { get; set; }
    [JsonProperty("image")]
    public byte[] image { get; set; }
    [JsonProperty("homepage")]
    public string homepage { get; set; }
    [JsonProperty("icon")]
    public byte[] icon { get; set; }
}


public class UserLoginInfo {
    [JsonProperty("user_num")]
    public string user_num { get; set; }
    [JsonProperty("password")]
    public string password { get; set; }
    [JsonProperty("last_login_time")]
    public DateTime last_login_time { get; set; }
    [JsonProperty("regist_time")]
    public DateTime regist_time { get; set; }
}

public class UserVipLevel {
    [JsonProperty("user_num")]
    public string user_num { get; set; }
    [JsonProperty("vip_flag")]
    public bool vip_flag { get; set; }
    [JsonProperty("use_time_counter")]
    public int use_time_counter { get; set; }
    [JsonProperty("vip_time_counter")]
    public int vip_time_counter { get; set; }
    [JsonProperty("vip_owen_day")]
    public DateTime  vip_owen_day { get; set; }
    [JsonProperty("day_counter")]
    public int  day_counter { get; set; }
}
