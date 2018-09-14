using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetMusic
{
    class SliderzResponse
    {

        public class Entry
        {
            public string url { get; set; }
            public string id { get; set; }
            public string artist { get; set; }
            public string title { get; set; }
            public string duration { get; set; }
            public string lyrics { get; set; }
            public string tit_art { get; set; }
            public string dlink { get; set; }
            public string info_url { get; set; }
        }

        public class Feed
        {
            public Entry entry { get; set; }
        }

        public class RootObject
        {
            public List<Feed> feed { get; set; }
        }

    }
}
