using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Remyngton_v2
{
    public class SimplifiedPoints
    {
        public List<Beatmap> beatmap = new List<Beatmap>();
    }

    public class Beatmap
    {
        public string beatmapName { get; set; }
        public List<Participant> Participant = new List<Participant>();
    }

    public class Participant
    {
        public string name { get; set; }
        public string totalPoints { get; set; }
    }
}