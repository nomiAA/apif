using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;


namespace tryApi.Utilities
{
    public class JSONHandler
    {
        public static T[] DeSerializeNonStandardList<T>(string jsonFilePath)
        {
            String JSONtxt = File.ReadAllText(jsonFilePath);

            //Capture JSON string for each object, including curly brackets 
            Regex regex = new Regex(@".*(?<=\{)[^}]*(?=\}).*", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(JSONtxt);

            string joinedJSON = string.Join(",", matches.Cast<Match>().Select(m => m.Value));
            joinedJSON = string.Format("[{0}]", joinedJSON);

            T[] Items = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<T>>(joinedJSON).ToArray<T>();
            return Items;
        }

        public static void DeleteJsonElement(string jsonFilePath, string element, string value)
        {
            var json = File.ReadAllText(jsonFilePath);
            try
            {
                JArray jsonArray = JArray.Parse(json);
                

                var elementToDeleted = jsonArray.FirstOrDefault(obj => obj[element].Value<string>() == value);

                jsonArray.Remove(elementToDeleted);

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonArray, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(jsonFilePath, output);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static void AddJsonElement(string jsonFilePath, JObject element)
        {
            var json = File.ReadAllText(jsonFilePath);
            try
            {
                JArray jsonArray = JArray.Parse(json);
                int index = 1;
                if(jsonArray.Count >1)
                    index = ((int)jsonArray.Last["id"]) + 1;
                element["id"] = index;
                jsonArray.Add(element);

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonArray, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(jsonFilePath, output);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public static void ReplaceJsonValue()
        {
            string filepath = "../../json1.json";
            string result = string.Empty;
            using (StreamReader r = new StreamReader(filepath))
            {
                var json = r.ReadToEnd();
                var jobj = JObject.Parse(json);
                foreach (var item in jobj.Properties())
                {
                    item.Value = item.Value.ToString().Replace("v1", "v2");
                }
                result = jobj.ToString();
                Console.WriteLine(result);
            }
            File.WriteAllText(filepath, result);
        }
    }
}