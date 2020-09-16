using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeFiverr
{
    public class video
    {
        public string title { get; set; }
        public string Author { get; set; }
        public string Url { get; set; }
        public Image thumbnail { get; set; }
        public float Duration{ get; set; }
        public string viewCount { get; set; }
    }
}
