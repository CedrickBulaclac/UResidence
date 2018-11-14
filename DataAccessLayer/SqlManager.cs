using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
namespace UResidence
{
    public static class SqlManager
    {
        //const string CON_STRING = "Data Source=198.38.83.33;Initial Catalog=margx26_uresidence;Integrated Security=False;User ID=margx26_uresidence;Password=elliry28;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //const string CON_STRING = "Data Source=CEDPC\\SQLEXPRESS;Initial Catalog=URESIDENCE;Integrated Security=True";
        //const string CON_STRING = "Data Source=MARTINY520\\SQLEXPRESS;Initial Catalog=c;Integrated Security=True";
        //const string CON_STRING = "Data Source=HP\\SQLEXPRESS;Initial Catalog=UResidence;Integrated Security=True";
          //const string CON_STRING = ConfigurationManager.ConnectionStrings["cedpc"].ConnectionString;

        public static List<T> Select<T>(SqlCommand command) where T : BaseProperty<T>, new()
        {
            List<T> ret = new List<T>();
            T item = default(T);
            string CON_STRING =ConfigurationManager.ConnectionStrings["URESIDENCEConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CON_STRING))
            {
                try
                {
                    con.Open();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        command.Connection = con;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                item = new T();
                                item = item.CreateObject(reader);
                                ret.Add(item);
                            }
                        }
                    }

                }
                catch
                {
                    throw;
                }
            }
            return ret;
        }

        public static bool ExecuteNonQuery(SqlCommand command)
        {
            bool ret = false;
            string CON_STRING = ConfigurationManager.ConnectionStrings["URESIDENCEConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CON_STRING))
            {
                try
                {
                    con.Open();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        command.Connection = con;
                        command.ExecuteNonQuery();
                        ret = true;
                    }

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return ret;
        }



    }
}
