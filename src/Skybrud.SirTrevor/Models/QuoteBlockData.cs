using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skybrud.SirTrevor.Models
{
    public class QuoteBlockData : TextBlockData, IBlockData
    {
        public string cite;

        public QuoteBlockData()
        {
            _id = new ShortGuid(Guid.NewGuid());
        }
        private ShortGuid _id;
        public ShortGuid id
        {
            get
            {
                return _id;
            }
        }
    }
}
