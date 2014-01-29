using Newtonsoft.Json;
using Skybrud.SirTrevor.DataTypes;
using Skybrud.SirTrevor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.SirTrevor
{
    public class PropertyEditorValueConverter : IPropertyEditorValueConverter
    {
        public global::Umbraco.Core.Attempt<object> ConvertPropertyValue(object value)
        {
            try
            {
                string json = string.Format("{0}", value);
                var model = JsonConvert.DeserializeObject<Container>(json, new BlockConverter());
                if (model != null && model.data != null)
                {
                    model.data._raw = json;
                    return new global::Umbraco.Core.Attempt<object>(true, model.data);
                }
                return new global::Umbraco.Core.Attempt<object>(false, null);
            }
            catch
            {
                return new global::Umbraco.Core.Attempt<object>(false, null);
            }
        }
        public bool IsConverterFor(Guid propertyEditorId, string docTypeAlias, string propertyTypeAlias)
        {
            return (propertyEditorId == DataType.DataTypeId);
        }
    }
}