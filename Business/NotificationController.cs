using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class NotificationController
    {
        public static List<Notification> GetAll()
        {
            List<Notification> ret = default(List<Notification>);
            const string GET_ALL = @"SELECT Id,Rate,Refno,Date,Visit,ISNULL(OwnerId,0),ISNULL(TenantId,0),Type FROM [tbNotification] order by Id Desc";
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Notification>(com);
            return ret;
        }
        public static List<Notification> GetAllO(int id)
        {
            List<Notification> ret = default(List<Notification>);
            const string GET_ALL = @"SELECT Id,Rate,Refno,Date,Visit,ISNULL(OwnerId,0),ISNULL(TenantId,0),Type FROM [tbNotification]  where OwnerId=@OwnerId order by Id Desc";
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@OwnerId", id));
            ret = SqlManager.Select<Notification>(com);
            return ret;
        }
        public static List<Notification> GetAllT(int id)
        {
            List<Notification> ret = default(List<Notification>);
            const string GET_ALL = @"SELECT Id,Rate,Refno,Date,Visit,ISNULL(OwnerId,0),ISNULL(TenantId,0),Type FROM [tbNotification]  where TenantId=@TenantId order by Id Desc";
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@TenantId", id));
            ret = SqlManager.Select<Notification>(com);
            return ret;
        }
        public static List<Notification> GetCountO(int id)
        {
            List<Notification> ret = default(List<Notification>);
            const string GET_ALL = @"SELECT Id,Rate,Refno,Date,Visit,ISNULL(OwnerId,0),ISNULL(TenantId,0),Type FROM [tbNotification] where Visit=0 and OwnerId=@OwnerId";
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@OwnerId", id));
            ret = SqlManager.Select<Notification>(com);
            return ret;
        }
        public static List<Notification> GetCountT(int id)
        {
            List<Notification> ret = default(List<Notification>);
            const string GET_ALL = @"SELECT Id,Rate,Refno,Date,Visit,ISNULL(OwnerId,0),ISNULL(TenantId,0),Type FROM [tbNotification] where Visit=0 and TenantId=@TenantId";
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@TenantId", id));
            ret = SqlManager.Select<Notification>(com);
            return ret;
        }
        public static bool UpdateVisit(Notification not)
        {
            const string GET_UPDATE = @"update [tbNotification] set Visit=@Visit  WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", not.Id));
            com.Parameters.Add(new SqlParameter("@Visit", not.Visit));

            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool InsertO(Notification not)
        {
            const string GET_INSERT = @"insert [tbNotification] (Rate,Refno,Date,Visit,OwnerId,Type) values (@Rate,@Refno,@Date,@Visit,@OwnerId,@Type) ";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Type", not.Type));
            com.Parameters.Add(new SqlParameter("@Rate", not.Rate));
            com.Parameters.Add(new SqlParameter("@Refno", not.refno));
            com.Parameters.Add(new SqlParameter("@Date", not.Date));
            com.Parameters.Add(new SqlParameter("@Visit", not.Visit));
            com.Parameters.Add(new SqlParameter("@OwnerId", not.OwnerId));
            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool InsertT(Notification not)
        {
            const string GET_INSERT = @"insert [tbNotification] (Rate,Refno,Date,Visit,TenantId,Type) values (@Rate,@Refno,@Date,@Visit,@TenantId,@Type) ";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Type", not.Type));
            com.Parameters.Add(new SqlParameter("@Rate", not.Rate));
            com.Parameters.Add(new SqlParameter("@Refno", not.refno));
            com.Parameters.Add(new SqlParameter("@Date", not.Date));
            com.Parameters.Add(new SqlParameter("@Visit", not.Visit));
            com.Parameters.Add(new SqlParameter("@TenantId", not.TenantId));

            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
