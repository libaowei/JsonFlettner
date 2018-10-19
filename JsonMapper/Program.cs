using Newtonsoft.Json;
using System;
using System.IO;

namespace JsonMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var folderPath = @"C:\Users\User\Documents\Visual Studio 2017\Projects\JsonMapper\JsonFlettner\JsonMapper\";
            var sourceFile = "source.json";
            var tragetFile = "target.json";
            var secondFile = "second.json";

            var jsonFile = File.ReadAllText(folderPath + sourceFile);

            var flat = JsonHandler.Flettn(jsonFile);

            foreach (var item in flat)
            {
                Console.WriteLine(item.Key + " " + item.Value.ToString());
            }

            var targetJson = JsonConvert.SerializeObject(flat);

            File.WriteAllText(folderPath + tragetFile, targetJson);


            var jsonDic = new JsonDictionary(flat);

            var isActive = jsonDic.GetObjectOrGroup("isActive");
            Console.WriteLine(isActive);
            var address = jsonDic.GetObjectOrGroup("address");
            Console.WriteLine(address);
            var address_street = jsonDic.GetObjectOrGroup("address.street");
            Console.WriteLine(address_street);
            //var secondData = JsonHandler.UnFlettn(targetJson);
            //var secondJson = JsonConvert.SerializeObject(secondData);

            //File.WriteAllText(folderPath + secondFile, secondJson);


            Console.Read();
        }
    }
}
