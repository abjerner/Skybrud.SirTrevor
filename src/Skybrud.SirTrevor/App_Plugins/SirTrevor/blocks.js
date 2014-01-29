//this function is called by umbraco initialization while setting up the sirtrevor editor. 
//add any custom blocks in blocks/*.js and blocks/*.css 
//at the bottom, push your block name: _blocks.push("blockname");

//you will also need to create a razor partial for each new type receiving the hydrated model
/*
//Sample partial
@using Sniper.Umbraco.SirTrevor.Models
@inherits Umbraco.Web.Mvc.UmbracoViewPage<NewBlock>
@{
    <blockquote>
        @Model.data
        <em>@Model.data.otherPropertyName</em>
    </blockquote>
}

//Model for hydration
namespace Sniper.Umbraco.SirTrevor.Models
{
    public class NewBlock : IBlock 
    {
        public string type { get; set; }
        public NewBlockData { get;set; }
    }
    public class NewBlockData : IBlockData
    {
        //other json properties should go here (such as text, cite, url etc)
        public string otherPropertyName { get; set; }

        public NewBlockData()
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

//sample block:

*/
if (typeof (_blocks) == 'undefined') {
    _blocks = ["Text", "Image", "Video"];
}

//Add any extras here
//custom blocks are added between before-blocks.js definition and here
/*
_blocks.push("Quote");
_blocks.push("Heading");
*/

_blocks.push("Video");

function enabledBlockTypes() {
    return _blocks;
}