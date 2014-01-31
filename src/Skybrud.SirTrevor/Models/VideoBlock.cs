using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skybrud.SirTrevor.Models
{
    public class VideoBlock : IBlock
    {
        public string type { get; set; }
        public VideoBlockData data { get; set; }
    }
}