#region using

using System.Text;

#endregion

namespace EM.Framework.Utilities
{
    /// <summary>
    ///     Util class for Text Encoding. This class is a wrapper of .Net Encoding Class
    ///     Used to provide constant for different encoding formats.
    /// </summary>
    public static class TextEncoding
    {
        public const string Utf8 = "UTF-8";
        public const string Utf16 = "UTF-16";
        public const string Utf32 = "UTF-32";
        public const string Ascii = "ASCII";

        /// <summary>
        ///     Gets the text encoding.
        /// </summary>
        /// <param name="textEncode">The text encode format.</param>
        /// <returns>
        ///     Encoding Object
        /// </returns>
        public static Encoding GetTextEncoding(string textEncode)
        {
            return Encoding.GetEncoding(textEncode);
        }
    }
}