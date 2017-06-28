using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketOnlineServer
{
    public class StorageStaticObject
    {
        private StorageStaticObject() { }
        public static Dictionary<String, UserConnector> UserSocketHashMap=new Dictionary<string, UserConnector> { };
    }
}
