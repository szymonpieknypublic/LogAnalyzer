using Newtonsoft.Json;
using System.IO;

namespace LogsAnalyzer.BLL.Helpers
{
    public static class SerializationHelper
    {
        public static T Deserialize<T>(Stream jsonBody)
        {
            using (StreamReader sr = new StreamReader(jsonBody))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                return new JsonSerializer().Deserialize<T>(reader);
            }
        }
    }
}