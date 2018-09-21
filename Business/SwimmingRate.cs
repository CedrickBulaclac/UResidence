﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class SwimmingRate : BaseProperty<SwimmingRate>
    {
        public int Id { get; set; }
        public int AmenityId { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }

        public SwimmingRate CreateObject(SqlDataReader reader)
        {
            SwimmingRate ret = new SwimmingRate();
            ret.Id = reader.GetInt32(0);
            ret.AmenityId = reader.GetInt32(1);
            ret.Adult = reader.GetInt32(2);
            ret.Child = reader.GetInt32(3);

            return ret;
        }

        public void Reset()
        {
            this.Id = 0;
            this.AmenityId = 0;
            this.Adult = 0;
            this.Child = 0;

        }


    }
}
