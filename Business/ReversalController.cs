﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class ReversalController
    {
        public static List<Reversal> GET_ALL()
        {
            const string GET_ALL = @"select Id,RefNo,Description,Status,CreatedBy,ApprovedBy,Amount from tbReversal";
            List<Reversal> ret = default(List<Reversal>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Reversal>(com);
            return ret;
        }
        public static List<Reversal> GET_ALL(int refno)
        {
            const string GET_ALL = @"select Id,RefNo,Description,Status,CreatedBy,ApprovedBy,Amount from tbReversal where RefNo=@RefNo";
            List<Reversal> ret = default(List<Reversal>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@RefNo", refno));
            ret = SqlManager.Select<Reversal>(com);
            return ret;
        }
        public static bool Update(Reversal ret)
        {
            const string GET_UPDATE = @"update [tbReversal] set Description=@Description,Status=@Status,Amount=@Amount WHERE RefNo = @RefNo";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@RefNo", ret.RefNo));
            com.Parameters.Add(new SqlParameter("@Description", ret.Description));
            com.Parameters.Add(new SqlParameter("@Status", ret.Status));
            com.Parameters.Add(new SqlParameter("@CreatedBy", ret.CreatedBy));
            com.Parameters.Add(new SqlParameter("@Amount", ret.Amount));
            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool UpdateA(Reversal ret)
        {
            const string GET_UPDATE = @"update [tbReversal] set ApprovedBy=@ApprovedBy,Status=@Status,Amount=@Amount WHERE RefNo = @RefNo";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@RefNo", ret.RefNo));
            com.Parameters.Add(new SqlParameter("@Status", ret.Status));
            com.Parameters.Add(new SqlParameter("@ApprovedBy", ret.ApprovedBy));
            com.Parameters.Add(new SqlParameter("@Amount", ret.Amount));
            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool Insert(Reversal ret)
        {
            const string GET_INSERT = @"insert [tbReversal] (RefNo,Description,Status,CreatedBy,Amount,ApprovedBy) values (@RefNo,@Description,@Status,@CreatedBy,@Amount,@ApprovedBy) ";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@RefNo", ret.RefNo));
            com.Parameters.Add(new SqlParameter("@Description", ret.Description));
            com.Parameters.Add(new SqlParameter("@Status", ret.Status));
            com.Parameters.Add(new SqlParameter("@CreatedBy", ret.CreatedBy));
            com.Parameters.Add(new SqlParameter("@Amount", ret.Amount));
            com.Parameters.Add(new SqlParameter("@ApprovedBy", "None"));
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
