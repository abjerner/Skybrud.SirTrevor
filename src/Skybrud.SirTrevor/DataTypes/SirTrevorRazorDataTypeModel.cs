using System.Xml.Linq;
using Skybrud.SirTrevor.DynamicJson;
using umbraco.MacroEngines;

namespace Skybrud.SirTrevor.DataTypes {

    [RazorDataTypeModel("2ef0b050-0d70-4605-8549-6267f6992e29")]
    public class SirTrevorRazorDataTypeModel : IRazorDataTypeModel {

        public bool Init(int currentNodeId, string propertyData, out object instance) {
            
            if (propertyData.StartsWith("{") || propertyData.StartsWith("}")) {
                instance = DynamicJsonConverter.Parse(propertyData);
            } else {
                instance = new {
                    data = new object[0]
                };
            }

            return true;

        }
    
    }

}