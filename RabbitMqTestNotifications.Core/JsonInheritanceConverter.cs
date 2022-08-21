using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using NJsonSchema.Converters;

namespace RabbitMqTestNotifications.Core
{
    public class JsonInheritanceConverter : NJsonSchema.Converters.JsonInheritanceConverter
    {
        protected override Type GetDiscriminatorType(JObject jObject, Type objectType, string discriminatorValue)
        {
            var type = typeof(JsonInheritanceConverter).Assembly.GetTypes()
                .FirstOrDefault(f => f.Name == discriminatorValue);
            return type ?? base.GetDiscriminatorType(jObject, objectType, discriminatorValue);
        }
    }
}