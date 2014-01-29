using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;

namespace Skybrud.SirTrevor.DynamicJson {

    /// <summary>
    /// Rusty dusty code from a year or two back. Haven't expiriencedany issues
    /// yet, but then again - I haven't tested it that much yet.
    /// </summary>
    public class DynamicJsonConverter : JavaScriptConverter {

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer) {

            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (type == typeof(object)) {
                return new DynamicJsonObject(dictionary);
            }

            return null;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer) {
            throw new NotImplementedException();
        }

        public override IEnumerable<Type> SupportedTypes {
            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(object) })); }
        }

        public static dynamic Parse(string json) {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });
            return jss.Deserialize(json, typeof(object));

        }

    }

}