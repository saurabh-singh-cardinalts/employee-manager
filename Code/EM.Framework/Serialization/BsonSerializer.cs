#region using

using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

#endregion

namespace EM.Framework.Serialization
{
    /// <summary>
    ///     Wrapper class for serializing the .Net Object to BSON - Binary JSON - Format
    ///     This is done using <c>Newtonsoft.Json.JsonSerializer</c>
    /// </summary>
    public class BsonSerializer
    {
        /// <summary>
        ///     Serializes the specified object to a byte[] - JSON format
        /// </summary>
        /// <typeparam name="T">The Object type to be serialized</typeparam>
        /// <param name="obj">The source object</param>
        /// <returns>
        ///     Byte Array representation of the JSON format
        /// </returns>
        public byte[] Serialize<T>(T obj)
        {
            byte[] result;
            using (var memStream = new MemoryStream())
            {
                Serialize(obj, memStream);
                result = memStream.ToArray();
            }
            return result;
        }

        /// <summary>
        ///     Serializes the specified object to a stream.
        /// </summary>
        /// <typeparam name="T">The Object type to be serialized</typeparam>
        /// <param name="obj">The source object</param>
        /// <param name="stream">Output stream.</param>
        public void Serialize<T>(T obj, Stream stream)
        {
            using (var writer = new BsonWriter(stream))
            {
                //writer.WriteRaw(JsonSerializer.Serialize(obj));
                var serializer = new Newtonsoft.Json.JsonSerializer
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DateFormatHandling = DateFormatHandling.IsoDateFormat
                    };
                serializer.Serialize(writer, obj);
            }
        }

        /// <summary>
        ///     Deserializes the given binary array to a specified .Net Object
        /// </summary>
        /// <typeparam name="T">Object Type to be Deserialized</typeparam>
        /// <param name="data">The binary array representation of the object.</param>
        /// <param name="rootIsArray">
        ///     if set to <c>true</c> [root is array].
        /// </param>
        /// <returns>
        ///     The Deserialized Object of the specified Type
        /// </returns>
        public T Deserialize<T>(byte[] data, bool rootIsArray = false)
        {
            T obj;
            using (var memStream = new MemoryStream(data))
            {
                obj = Deserialize<T>(memStream, rootIsArray);
            }
            return obj;
        }

        /// <summary>
        ///     Deserializes the specified stream to a specified .Net Object
        /// </summary>
        /// <typeparam name="T">Object Type to be Deserialized</typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="rootIsArray">
        ///     if set to <c>true</c> [root is array].
        /// </param>
        /// <returns>
        ///     The Deserialized Object of the specified Type
        /// </returns>
        public T Deserialize<T>(Stream stream, bool rootIsArray = false)
        {
            T obj;
            stream.Seek(0, SeekOrigin.Begin);
            using (var reader = new BsonReader(stream))
            {
                reader.ReadRootValueAsArray = rootIsArray;
                var serializer = new Newtonsoft.Json.JsonSerializer
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DateFormatHandling = DateFormatHandling.IsoDateFormat
                    };
                obj = serializer.Deserialize<T>(reader);
            }
            return obj;
        }
    }
}