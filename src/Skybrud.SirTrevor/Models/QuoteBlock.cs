using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skybrud.SirTrevor.Models
{
    public class QuoteBlock : IBlock
    {

        public string type { get; set; }
        public QuoteBlockData data { get; set; }
    }
}