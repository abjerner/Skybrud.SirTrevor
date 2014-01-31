using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skybrud.SirTrevor.Models
{
    public class ImageBlock : IBlock
    {
        public string type { get;set;}
        public ImageBlockData data { get; set;}
    }
}