using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Skybrud.SirTrevor.DynamicJson {

    public class DynamicJsonObject : DynamicObject {

        public IDictionary<string, object> Dictionary { get; set; }

        public DynamicJsonObject(IDictionary<string, object> dictionary) {
            Dictionary = dictionary;
        }

        public bool HasMember(string name) {
            return Dictionary.ContainsKey(name);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result) {

            if (!Dictionary.ContainsKey(binder.Name)) {
                result = null;
                return true;
            }

            result = Dictionary[binder.Name];

            if (result is IDictionary<string, object>) {
                result = new DynamicJsonObject(result as IDictionary<string, object>);
            } else if (result is ArrayList && (result as ArrayList) is IDictionary<string, object>) {
                result = new List<DynamicJsonObject>((result as ArrayList).ToArray().Select(x => new DynamicJsonObject(x as IDictionary<string, object>)));
            } else if (result is ArrayList) {
                var list = new List<object>();
                foreach (var o in (result as ArrayList).ToArray()) {
                    if (o is IDictionary<string, object>) {
                        list.Add(new DynamicJsonObject(o as IDictionary<string, object>));
                    } else {
                        list.Add(o);
                    }
                }
                result = list;
            }

            return true;
        }

    }

}