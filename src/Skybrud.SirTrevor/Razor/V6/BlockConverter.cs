using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Skybrud.SirTrevor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skybrud.SirTrevor
{
    public class BlockConverter : CustomCreationConverter<IBlock>
    {

        public override IBlock Create(Type objectType)
        {
            throw new NotImplementedException();
        }

        public IBlock CreateBlock(Type objectType, JObject jObject)
        {
            var type = (string)jObject.Property("type");
            try
            {
                Type blockModelType = Type.GetType(string.Format("Skybrud.SirTrevor.Models.{0}Block", System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(type)));
                var block = Activator.CreateInstance(blockModelType);
                (block as IBlock).type = type;
                return (block as IBlock);
            }
            catch
            {
                return new NullBlock() { type = type };
            }
            //throw new ApplicationException(String.Format("The block type {0} is not supported for deserialization", type));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            // Load JObject from stream 
            JObject jObject = JObject.Load(reader);
            object target = null;

            if (objectType == typeof(IBlock))
            {
                // Create target object based on JObject 
                target = CreateBlock(objectType, jObject);

            }

            if (target != null)
            {
                // Populate the object properties 
                serializer.Populate(jObject.CreateReader(), target);

                return target;
            }

            return null;


        }
    }
}
