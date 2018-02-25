using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public interface BaseProperty<T>
    {
        T CreateObject(SqlDataReader reader);
        //bool Validate(out string[] errors)

        void Reset();
    }
}
