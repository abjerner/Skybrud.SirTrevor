using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skybrud.SirTrevor.Models
{
    public class ImageBlockData: IBlockData
    {
        public FileWrapper file { get; set; }


        public string url
        {
            get
            {
                if (file != null)
                {
                    return file.url;
                }
                return null;
            }
        }

        public ImageBlockData()
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
