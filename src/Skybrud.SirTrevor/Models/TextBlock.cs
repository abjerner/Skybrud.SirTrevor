using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skybrud.SirTrevor.Models
{
    public class TextBlock : IBlock
    {

        public string type { get; set; }
        public TextBlockData data { get; set; }
    }
}