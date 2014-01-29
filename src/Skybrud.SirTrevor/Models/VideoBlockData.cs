using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skybrud.SirTrevor.Models
{
    public class VideoBlockData : IBlockData
    {
        public string source { get; set; }
        public string remote_id { get; set; }

        public VideoBlockData()
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
