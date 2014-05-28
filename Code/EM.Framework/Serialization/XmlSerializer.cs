#region using

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using EM.Framework.Utilities;

#endregion

namespace EM.Framework.Serialization
{
    /// <summary>
    ///     Wrapper class for serializing the .Net Object to XML Format
    ///     This is done using <c>System.Xml.Serialization.XmlSerializer</c>
    /// </summary>
    public class XmlSerializer
    {
        /// <summary>
        ///     Serializes the specified object to XDocument.
        /// </summary>
        /// <typeparam name="T">The Object type to be serialized</typeparam>
        /// <param name="obj">The source object</param>
        /// <param name="rootName">Root Element Name</param>
        /// <param name="allowNamespace">
        ///     if set to <c>true</c> [allow namespace].
        /// </param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>
        ///     XDocument representation of the object
        /// </returns>
        public XDocument Serialize<T>(T obj, string rootName = null, bool allowNamespace = true,
                                      string encoding = TextEncoding.Utf8)
        {
            XmlRootAttribute xRoot = null;
            if (!string.IsNullOrWhiteSpace(rootName))
            {
                xRoot = new XmlRootAttribute
                    {
                        ElementName = rootName
                    };
            }

            XDocument result;
            var serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType(), xRoot);
            using (var memStream = new MemoryStream())
            {
                using (var writer = new XmlTextWriter(memStream, TextEncoding.GetTextEncoding(encoding)))
                {
                    writer.Formatting = Formatting.Indented;
                    var namespaces = new XmlSerializerNamespaces();
                    if (!allowNamespace)
                    {
                        namespaces.Add(string.Empty, string.Empty);
                    }
                    serializer.Serialize(writer, obj, namespaces);
                    writer.Flush();
                    memStream.Seek(0, SeekOrigin.Begin);
                    result = XDocument.Load(memStream);
                }
            }
            return result;
        }

        /// <summary>
        ///     Deserializes the specified xml data to a specified .Net Object.
        /// </summary>
        /// <typeparam name="T">Object Type to be deserialized</typeparam>
        /// <param name="data">string representation of XML data.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The deserialized object of the specified type</returns>
        public T Deserialize<T>(string data, string encoding = TextEncoding.Utf8)
        {
            T obj;
            Type type = typeof (T);
            var serializer = new System.Xml.Serialization.XmlSerializer(type);
            Encoding textEncoding = TextEncoding.GetTextEncoding(encoding);
            using (var memStream = new MemoryStream(textEncoding.GetBytes(data)))
            {
                memStream.Seek(0, SeekOrigin.Begin);
                obj = (T) serializer.Deserialize(memStream);
            }
            return obj;
        }
    }
}