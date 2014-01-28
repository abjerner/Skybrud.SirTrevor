using System;
using System.Xml;
using umbraco.cms.businesslogic.datatype;

namespace Skybrud.SirTrevor.DataTypes {
    
    public class SirTrevorData : DefaultData {

        public SirTrevorData(BaseDataType dataType) : base(dataType) { }

        public override XmlNode ToXMl(XmlDocument data) {

            if (Value == null || String.IsNullOrWhiteSpace(Value.ToString())) {
                return base.ToXMl(data);
            }

            // Save the JSON data
            return data.CreateCDataSection(Value.ToString());
        
        }

    }

}