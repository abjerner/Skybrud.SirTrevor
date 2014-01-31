using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skybrud.SirTrevor.Models
{
    public class Blocks : List<IBlock>
    {
        internal string _raw;
        public string ToJson()
        {
            return _raw;
        }
    }
}