using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using tryApi.Models;

namespace tryApi
{
    public static class Helper
    {
        public static T[] DeSerializeNonStandardList<T>(string jsonFilePath)
        {
            String JSONtxt = File.ReadAllText(jsonFilePath);

            //Capture JSON string for each object, including curly brackets 
            Regex regex = new Regex(@".*(?<=\{)[^}]*(?=\}).*", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(JSONtxt);

            //string joinedJSON = string.Join(",", matches.Cast<Match>().Select(m => m.Value));
            //joinedJSON = string.Format("[{0}]", joinedJSON);

            T[] Items = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<T>>(JSONtxt).ToArray<T>();
            return Items;
        }

    }
}