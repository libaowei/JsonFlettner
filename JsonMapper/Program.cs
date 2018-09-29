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


            var secondData = JsonHandler.UnFlettn(targetJson);
            var secondJson = JsonConvert.SerializeObject(secondData);

            File.WriteAllText(folderPath + secondFile, secondJson);


            Console.Read();
        }
    }
}
