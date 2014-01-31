using MarkdownSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Skybrud.SirTrevor.Models
{
    public class HeadingBlockData : IHtmlString, IBlockData
    {
        public string text;

        public string ToHtmlString()
        {
            var helper = new Markdown();
            return helper.Transform(text);
        }

        public HeadingBlockData()
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
