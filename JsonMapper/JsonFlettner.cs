using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace JsonMapper
{
    public static class JsonFlettner
    {
        //https://www.newtonsoft.com/json
        public static Dictionary<string, object> Flettner(string json)
        {
            dynamic fullObject = JsonConvert.DeserializeObject<ExpandoObject>(json, new ExpandoObjectConverter());

            return GetInner(fullObject as ExpandoObject);
        }

        private static Dictionary<string, object> GetInner(ExpandoObject expandoObject)
        {
            var result = new Dictionary<string, object>();
            var objectdictionary = expandoObject as IDictionary<string, object>;
            Type expendoType = typeof(ExpandoObject);

            foreach (var item in objectdictionary)
            {
                if (item.Value.GetType() != expendoType)
                {
                    result.Add(item.Key, item.Value);
                }
                else
                {
                    var innerResult = GetInner(item.Value as ExpandoObject);

                    foreach (var innerItem in innerResult)
                    {
                        result.Add($"{item.Key}.{innerItem.Key}", innerItem.Value);
                    }
                }
            }

            return result;

        }
    }
}
