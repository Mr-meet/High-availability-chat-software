using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


class Relationship
{

}
/*HashMap<String  key , List<Friend >  value >  friendships;
其中key为用户账号。
Value为好友的账号的list。
注：好友关系不是相互的。非好友使用id聊天。
*/
public class Friend
{
    [JsonProperty("id")]
    public string id { get; set; } //好友的ID
    [JsonProperty("group")]
    public int group { get; set; } //好友所在的组。1组为默认组“我的好友”，-1为默认组“黑名单”。0组为默认组“陌生人”，2组为默认组“特别关心”。
    [JsonProperty("friendShip")]
    public int friendShip { get; set; } //好友的关系。1：特别关注好友；0：正常好友；2：黑名单；3：陌生人。
    [JsonProperty("remark")]
    public string remark { get; set; } //好友的备注。初始化为好友昵称！
}


/*
HashMap<String key ,HashMap<GroupInfo> group_info>
其中key的格式为：userGroup_+“UserID”。
其中GroupInfo类对象内容如下。
 */
public class GroupInfo
{
    [JsonProperty("groupId")]
    public int groupId { get; set; } //系统定义的组ID，不能重复，且不能为-1、0、1、2
    [JsonProperty("groupName")]
    public string groupName { get; set; } //存储用户的组名
}

/* HashMap<String key,List<String> community_id>;
其中key的格式为：userCommunity_+“UserID”
用户所包含的群表（当群表中无法找到community_id时，从list中移除此ID）
*/

/*
 HashMap<String  key , Hash<String member_key，Member>  value > 
Key 的格式为群ID。
//Member_key 为群成员ID。 其中Member类对象内容如下
 */
public class Member{
    [JsonProperty("memberId")]
    public string memberId { get; set; } //成员ID
    [JsonProperty("admin")]
    public bool admin { get; set; } //是否为管理员（初始化为FALSE）
    [JsonProperty("joinTime")]
    public DateTime joinTime { get; set; } //加群日期
    [JsonProperty("communityNickName")]
    public string communityNickName { get; set; } //群成员的群昵称（初始化成为用户昵称）//当用户尝试修改此值时，交由rabbitmq进行分发到Java服务端进行操作。
}


