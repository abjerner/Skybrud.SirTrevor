using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skybrud.SirTrevor.Models
{
    public class HeadingBlock : IBlock
    {

        public string type { get; set; }
        public HeadingBlockData data { get; set; }
    }
}