using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonMapper
{
    public class JsonDictionary
    {
        private Dictionary<string, object> _dictionary;


        public JsonDictionary(Dictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        public T GetObject<T>(string key)
        {
            var hasValue = _dictionary.TryGetValue(key, out object result);

            return hasValue ? (T)Convert.ChangeType(result, typeof(T)) : default(T);
        }

        public object GetObjectOrGroup(string key)
        {
            StringBuilder sb = new StringBuilder(key, key.Length + 1);
            sb.Append(new char[] { '.' });
            var keyWithDot = sb.ToString();
            var keys = _dictionary.Keys.Where(k => k == key || k.StartsWith(keyWithDot)).ToList();

            if (keys.Count == 1)
            {
                return _dictionary[key];
            }
            else
            {
                var expndo = new Dictionary<string, object>();

                foreach (var item in keys)
                {
                    expndo.Add(item, _dictionary[item]);
                }
                return expndo;
            }
        }

    }
}
