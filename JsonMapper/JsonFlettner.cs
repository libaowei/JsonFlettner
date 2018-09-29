using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace JsonMapper
{
    public static class JsonHandler
    {
        public static Dictionary<string, object> UnFlettn(string json)
        {
            dynamic expandoObject = JsonConvert.DeserializeObject<ExpandoObject>(json, new ExpandoObjectConverter());
            var objectDictionary = expandoObject as IDictionary<string, object>;


            return UnFlettnInner(objectDictionary);
        }

        private static Dictionary<string, object> UnFlettnInner(IDictionary<string, object> objectDictionary)
        {
            var result = new Dictionary<string, object>();

            foreach (var item in objectDictionary)
            {
                var index = item.Key.IndexOf('.');
                if (index > -1)
                {
                    var firstSection = item.Key.Substring(0, index);
                    if (!result.ContainsKey(firstSection))
                    {
                        var innerDictionary = objectDictionary.Where(kvp => kvp.Key.StartsWith(firstSection)).ToDictionary(k => k.Key.Substring(index + 1), v => v.Value);
                        var innerResult = UnFlettnInner(innerDictionary);

                        result.Add(firstSection, innerResult);
                    }
                }
                else
                {
                    result.Add(item.Key, item.Value);
                }
            }

            return result;
        }


        public static Dictionary<string, object> Flettn(string json)
        {
            dynamic fullObject = JsonConvert.DeserializeObject<ExpandoObject>(json, new ExpandoObjectConverter());

            return GetInner(fullObject as ExpandoObject);
        }

        private static Dictionary<string, object> GetInner(ExpandoObject expandoObject)
        {
            var result = new Dictionary<string, object>();
            var objectDictionary = expandoObject as IDictionary<string, object>;
            Type expendoType = typeof(ExpandoObject);

            foreach (var item in objectDictionary)
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
