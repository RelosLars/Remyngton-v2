using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Remyngton_v2
{
    public class PointsResult
    {
        public Users[] Users { get; set; }
    }

    //public class PointHistory
    //{
    //    public Users[] User { get; set; }
    //}

    public class Users
    {
        public string UserID { get; set; }
        public Maps[] Map { get; set; }
    }

    public class Maps
    {
        public string MapID { get; set; }
        public Category[] Category { get; set; }
    }

    public class Category
    {
        public double Score { get; set; }
        public double Maxcombo { get; set; }
        public double Accuracy { get; set; }
        public double Misscount { get; set; }
    }
}