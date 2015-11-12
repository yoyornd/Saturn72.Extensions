using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Saturn72.Extensions.Utils
{
    public partial class JsonUtil
    {
        public static T ParseJsonFromFile<T>(string filePath) where T : class, new()
        {
            var jsonContent = File.ReadAllText(filePath);
            return ParseJsonFromString<T>(jsonContent);
        }

        public static T ParseJsonFromString<T>(string jsonContent) where T : class, new()
        {
            return JsonConvert.DeserializeObject<T>(JObject.Parse(jsonContent).ToString());
        }
    }
}