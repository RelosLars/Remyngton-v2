using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Remyngton_v2
{
    public class DeserializeTeams
    {
        public Team[] Teams { get; set; }
    }

    public class Team
    {
        public string Teamname { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
    }
}