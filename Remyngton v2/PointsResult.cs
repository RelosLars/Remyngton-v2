using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Remyngton_v2
{
    public class PointsResult
    {
        public List<Map> map = new List<Map>();
    }

    public class Map
    {
        public string beatmap_id { get; set; }
        public List<User> users = new List<User>();
    }

    public class User
    {
        public string user_id { get; set; } //can also act as teamname
        public string scorePoints { get; set; }
        public string maxcomboPoints { get; set; }
        public string accPoints { get; set; }
        public string countmissPoints { get; set; }
    }
}