using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace SocketOnlineServer
{
   public  class MySqlHelper
    {
        private MySqlHelper() { }
        private static string connectionString = "Server=139.199.35.140;user id=root;password=fj950602;Database=YellLandMark;Port=3306;charset=utf8;";
        private static MySqlConnection connection = new MySqlConnection(connectionString);
        private static MySqlCommand com = new MySqlCommand();
        public static void  openConnection() {
            if(connection.State==ConnectionState.Closed)
               connection.Open();
        }
        public static void closeConnection()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            
                DataSet ds = new DataSet();
                try
                {
                    
                    MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                    command.Fill(ds);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
                }
            
            return ds;
            
        }




        /// <summary>
        /// 执行查询语句，返回MySqlDataReader
        /// </summary>
        /// <param name="SQLString"></param>
        /// <returns></returns>
        public static MySqlDataReader QueryObject(string SQLString)
        {
            if (com != null) {
                com.Connection = connection;
                com.CommandText = SQLString;
            }
            if (com == null) {
                com = new MySqlCommand(SQLString,connection);
            }
                MySqlDataReader dr = null;
            //将com实例化，并将两个SELECT语句传递给com
            try
            {
                dr = com.ExecuteReader();
                return dr;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
   
        }


        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            if (com != null)
            {
                com.Connection = connection;
                com.CommandText = SQLString;
            }
            if (com == null)
            {
                com = new MySqlCommand(SQLString, connection);
            }
            try
                    {
                        //connection.Open();
                        int rows = com.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                //connection.Close();
                    Console.WriteLine(e.Message);
                        throw e;
                    }
                    //finally
                    //{
                    //    com.Dispose();
                    //}
                }
       
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string[] arrSql)
        {

            
            try
            {

                MySqlCommand cmdEncoding = new MySqlCommand();
                cmdEncoding.Connection = connection;
                cmdEncoding.ExecuteNonQuery();
                int rows = 0;
                foreach (string strN in arrSql)
                {
                    using (MySqlCommand cmd = new MySqlCommand(strN, connection))
                    {
                        rows += cmd.ExecuteNonQuery();
                    }
                }
                return rows;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
          
               
            
        }
    }
}
