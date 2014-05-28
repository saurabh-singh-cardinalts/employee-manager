#region using

using System;

#endregion

namespace EM.Framework.Utilities
{
    /// <summary>
    ///     Util Class for Hexadecimal string usage
    /// </summary>
    public static class HexString
    {
        /// <summary>
        ///     Converts bytes array to hexadecimal string.
        /// </summary>
        /// <param name="value">The value of type byte array</param>
        /// <returns>
        ///     Hexadecimal string representation of the byte array
        /// </returns>
        public static string ByteArrayToHexString(byte[] value)
        {
            return string.Concat(Array.ConvertAll(value, x => x.ToString("x2")));
        }

        /// <summary>
        ///     Converts the hexadecimal string to  byte array.
        /// </summary>
        /// <param name="value">hexadecimal string</param>
        /// <returns>
        ///     Byte array of the hexadecimal string
        /// </returns>
        public static byte[] HexStringToByteArray(string value)
        {
            int numberChars = value.Length;
            var bytes = new byte[numberChars/2];
            for (int i = 0; i < numberChars; i += 2)
                bytes[i/2] = Convert.ToByte(value.Substring(i, 2), 16);
            return bytes;
        }
    }
}