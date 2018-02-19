using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public static class SqlManager
    {
        const string CON_STRING = "Data Source=CEDPC\\SQLEXPRESS;Initial Catalog=UResidence;Integrated Security=True";

        public static List<T> Select<T>(SqlCommand command) where T : BaseProperty<T>, new()
        {
            List<T> ret = new List<T>();
            T item = default(T);

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
