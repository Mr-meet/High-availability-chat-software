using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class ICommandForMessage
    {
    /// <summary>
    /// datebase crud command
    /// </summary>
    public const string Insert = "insert";
    public const string Select = "select";
    public const string Update = "update";
    public const string Delete = "delect";
     
    /// <summary>
    /// rabbitmq  command
    /// </summary>
    public const string SendMessage = "sendMessage";
    public const string ChangeCommunityName = "changeCommunityName";
    public const string ReciveMessage = "reciveMessage";

    /// <summary>
    /// redis cache usage command
    /// </summary>
    public const string FindKey = "findKey";
    public const string InsertKey = "insertKey";
    public const string DeleteKey = "deleteKey";

    /// <summary>
    /// login and logout command
    /// </summary>
    public const string Login = "login";
    public const string Logout = "logout";
    public const string Registe = "registe";
    public const string Maintain = "maintain";//when a client login sucess,well send this ,and server well store the connection.

    /// <summary>
    /// option people
    /// </summary>
    public const string AddFriend = "addFriend";
    public const string DeleteFriends = "deleteFriends";
    public const string FindFriend = "findFriend";
    public const string AddGroup = "addGroup";
    public const string DeleteGroup = "deleteGroup";
    public const string ChangeDemo = "changeDemo";
    public const string ChangeGroup = "changeGroup";
    public const string GetPeople = "getPeople";


    /// <summary>
    /// option influxDB
    /// </summary>
    public const string GetMessageRecord = "getMessageRecord";
    public const string GetAllMessageRecord = "getAllMessageRecord";

    /// <summary>
    /// other options
    /// </summary>

    public const string Hello = "hello";//Connector to connect
    public const string Hey = "hey";//Returns the success of the connection
    public const string LoadDictionary = "loadDictionary";
    public const string LoadUserInfo = "loadUserInfo";

}

