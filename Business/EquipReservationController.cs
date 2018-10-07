using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class EquipReservationController
    {
        public static List<EquipReservation> GetAll()
        {
            const string GET_ALL = @"SELECT EquipmentId,Quantity,RefNo,Rate FROM[tbEquipReservation] order by EquipId";


            List<EquipReservation> ret = default(List<EquipReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<EquipReservation>(com);
            return ret;
        }

        public static EquipReservation Get(string eno)
        {
            const string GET_RECORD = @"SELECT EquipmentId,Quantity,RefNo,Rate FROM[tbEquipReservation] order by EquipId WHERE EquipNo = @EquipNo";

            EquipReservation ret = default(EquipReservation);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@EquipId", eno));
            ret = SqlManager.Select<EquipReservation>(com).First();

            return ret;
        }

        public static List<EquipReservation> Getr(int refno)
        {
            const string GET_RECORD = @"SELECT EquipmentId,Quantity,RefNo,Rate FROM [tbEquipReservation] WHERE RefNo = @refno  order by EquipmentId";

            List<EquipReservation> ret = default(List<EquipReservation>);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@refno", refno));
            ret = SqlManager.Select<EquipReservation>(com);

            return ret;
        }


        public static bool Update(EquipReservation eqp)
        {
            const string GET_UPDATE = @"update [tbEquipReservation] set EquipmentId= @EquipId, Quantity= @Quantity, RefNo= @RefNo WHERE EquipNo = @EquipNo";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@EquipId", eqp.EquipId));
            com.Parameters.Add(new SqlParameter("@Quantity", eqp.Quantity));
            com.Parameters.Add(new SqlParameter("@RefNo", eqp.RefNo));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(EquipReservation eqp)
        {
            const string GET_DELETE = @"delete [tbEquipReservation] WHERE RefNo = @RefNo";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@RefNo", eqp.RefNo));

            return SqlManager.ExecuteNonQuery(com);
        }
        /*
        public static bool Modify(Equipment eqp)
        {
            if (eqp.EquipmentNo == 0)
                return Insert(eqp);
            else
                return Update(eqp);
        }
        */
        public static bool Insert(EquipReservation eqp)
        {
            const string GET_INSERT = @"insert [tbEquipReservation] (EquipmentId,Quantity,RefNo,Rate) values (@EquipId, @Quantity, @RefNo,@Rate)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@EquipId", eqp.EquipId));
            com.Parameters.Add(new SqlParameter("@Quantity", eqp.Quantity));
            com.Parameters.Add(new SqlParameter("@RefNo", eqp.RefNo));
            com.Parameters.Add(new SqlParameter("@Rate", eqp.Rate));
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
    
