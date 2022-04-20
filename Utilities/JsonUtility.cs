using System;
using System.IO;

using Newtonsoft.Json;

using Serilog.Events;

namespace Grimoire.Utilities
{
    /// <summary>
    /// Provide the ability to serialize/deserialize simple .net objects to/from json
    /// </summary>
    public static class JsonUtility
    {
        /// <summary>
        /// Serialize the provided object to a json file at the provided path
        /// </summary>
        /// <param name="value">Object being serialized</param>
        /// <param name="path">Path where the json file will be created</param>
        public static void SerializeObject(object value, string path)
        {
            var serializer = new JsonSerializer();

            serializer.Formatting = Formatting.Indented;

            try {
                using (StreamWriter sw = new StreamWriter(path))
                    using (JsonWriter jw = new JsonTextWriter(sw))
                        serializer.Serialize(jw, value);
            }
            catch (Exception ex)
            {
                LogUtility.MessageBoxAndLog(ex, "serializing provided object", "Json Serialize Exception", LogEventLevel.Error);
                return;
            }
            
        }

        /// <summary>
        /// Deserialize the content string into an object of the provided type
        /// </summary>
        /// <typeparam name="T">Type of object to be deserialized to</typeparam>
        /// <param name="content">Json string containing data for the object</param>
        /// <returns>Object of T type containing data from content string</returns>
        public static T DeserializeObject<T>(string content)
        {
            try {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                LogUtility.MessageBoxAndLog(ex, "deserializing provided content", "Json Deserialize Exception", LogEventLevel.Error);
                return default;
            }
        }
    }
}
