using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketOnlineServer
{
    class StringHelper
    {

        /// <summary>
        /// 判断一个string类型的对象的值是否为空字符串或为null
        /// </summary>
        /// <param name="value"></param>
        /// <returns>为空字符串或为null返回true，否则返回false</returns>
        public static bool isNullOrEmpty(String value) {
            bool flag = false;
            if (value == null) {
                flag = true;
                return flag;
            }
            if (value.Trim().Equals("")) {
                flag = true;
                return flag;
            }
            return flag;
        }

        public static string getNewUuid() {
            return System.Guid.NewGuid().ToString();
        }

    }
}
