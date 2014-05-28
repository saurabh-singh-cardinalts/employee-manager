#region using

using System;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

#endregion

namespace EM.Framework.Serialization
{
    /// <summary>
    ///     Wrapper class for serializing the .Net Object to JSON Format
    ///     This is done using Newtonsoft JSON Convertor
    /// </summary>
    public class JsonSerializer
    {
        /// <summary>
        ///     Serializes the specified object to JSON string.
        /// </summary>
        /// <typeparam name="T">The Object type to be serialized</typeparam>
        /// <param name="obj">The source object</param>
        /// <returns>
        ///     JSON string representation of the object
        /// </returns>
        public string Serialize<T>(T obj)
        {
            //var isoDateTimeConverter = new IsoDateTimeConverter {DateTimeFormat = "g"};
            //default is "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";
            string result = null;
            Type type = typeof (T);
            if (type == typeof (XmlDocument))
            {
                var xmlDocument = obj as XmlDocument;
                if (xmlDocument != null)
                {
                    result = JsonConvert.SerializeXmlNode(xmlDocument);
                }
            }
            else if (type == typeof (XDocument))
            {
                var xDocument = obj as XDocument;
                if (xDocument != null)
                {
                    result = JsonConvert.SerializeXNode(xDocument);
                }
            }
            else
            {
                result = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DateFormatHandling = DateFormatHandling.IsoDateFormat
                    });
            }
            return result;
        }

        /// <summary>
        ///     Deserializes the specified data to a specified .Net Object.
        /// </summary>
        /// <typeparam name="T">Object Type to be deserialized</typeparam>
        /// <param name="data">string representation of JSON data.</param>
        /// <returns>
        ///     The deserialized object of the specified type
        /// </returns>
        public T Deserialize<T>(string data) where T : class
        {
            T obj;
            Type type = typeof (T);
            if (type == typeof (XmlDocument))
            {
                XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode(data);
                obj = xmlDoc as T;
            }
            else if (type == typeof (XDocument))
            {
                XDocument xDoc = JsonConvert.DeserializeXNode(data);
                obj = xDoc as T;
            }
            else
            {
                obj = JsonConvert.DeserializeObject(data, type, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DateFormatHandling =
                            DateFormatHandling.IsoDateFormat,
                    }) as T;
            }
            return obj;
        }
    }
}