using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skybrud.SirTrevor.Models
{
    public class MarkdownBlock : IBlock
    {

        public string type { get; set; }
        public MarkdownBlockData data { get; set; }
    }
}