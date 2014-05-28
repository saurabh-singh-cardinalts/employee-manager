#region using

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#endregion

namespace EM.Framework.Serialization
{
    /// <summary>
    ///     Wrapper class for serializing the .Net Object to Binary Format
    ///     This is done using <c>BinaryFormatter</c>
    /// </summary>
    public class BinarySerializer
    {
        /// <summary>
        ///     Serializes the specified object to a file system.
        /// </summary>
        /// <typeparam name="T">The Object type to be serialized</typeparam>
        /// <param name="obj">The source object</param>
        /// <param name="filePath">The file path.</param>
        public void Serialize<T>(T obj, string filePath)
        {
            using (Stream fileStream = File.Create(filePath))
            {
                Serialize(obj, fileStream);
            }
        }

        /// <summary>
        ///     Serializes the specified object to a stream.
        /// </summary>
        /// <typeparam name="T">The Object type to be serialized</typeparam>
        /// <param name="obj">The source object</param>
        /// <param name="stream">Output stream.</param>
        public void Serialize<T>(T obj, Stream stream)
        {
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, obj);
        }

        /// <summary>
        ///     Deserializes the specified binary file to a specified .Net Object
        /// </summary>
        /// <typeparam name="T">Object Type to be Deserialized</typeparam>
        /// <param name="filePath">The binary file path.</param>
        /// <returns>
        ///     The Deserialized Object of the specified Type
        /// </returns>
        public T Deserialize<T>(string filePath) where T : class
        {
            T obj;
            using (Stream fileStream = File.OpenRead(filePath))
            {
                obj = Deserialize<T>(fileStream);
            }
            return obj;
        }

        /// <summary>
        ///     Deserializes the specified stream to a specified .Net Object
        /// </summary>
        /// <typeparam name="T">Object Type to be Deserialized</typeparam>
        /// <param name="stream">The stream.</param>
        /// <returns>
        ///     The Deserialized Object of the specified Type
        /// </returns>
        public T Deserialize<T>(Stream stream) where T : class
        {
            stream.Seek(0, SeekOrigin.Begin);
            var binaryFormatter = new BinaryFormatter();
            return binaryFormatter.Deserialize(stream) as T;
        }
    }
}