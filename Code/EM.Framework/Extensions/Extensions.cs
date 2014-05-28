#region using

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http.Routing;
using System.Xml;
using System.Xml.Linq;
using EM.Framework.Utilities;

#endregion

namespace EM.Framework.Extensions
{
    /// <summary>
    ///     Extension to string Class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Extension to the string Compare methods to include case - sensitive comparison
        /// </summary>
        /// <param name="item">The source item.</param>
        /// <param name="value">The value.</param>
        /// <param name="comparison">StringComparison value.</param>
        /// <returns>
        ///     <c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(this string item, string value, StringComparison comparison)
        {
            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentException("The value cannot be null or empty", item);
            return item.IndexOf(value, comparison) != -1;
        }

        public static int? ToNullableInt(this string item)
        {
            int result;
            return int.TryParse(item, out result) ? result : (int?) null;
        }

        public static string EncryptToMd5Hash(this string item)
        {
            //we use codepage 1252 because that is what sql server uses
            //TODO: Make sure this is same as LAMP STACK MD5
            MD5 md5 = new MD5CryptoServiceProvider();
            //compute hash from the bytes of text
            md5.ComputeHash(Encoding.ASCII.GetBytes(item));
            //get hash result after compute it
            byte[] result = md5.Hash;
            return HexString.ByteArrayToHexString(result);
        }

        public static bool IsEqual(this string item, string strToCompare,
                                   StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return string.Equals(item.Trim(), strToCompare.Trim(), comparison);
        }
    }

    /**
    /// <summary>
    /// Extension to Enum
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the value of description attribute from the Enum
        /// </summary>
        /// <param name="item">The source item.</param>
        /// <returns>
        /// Returns the string value of the description specified.
        /// </returns>
        public static string GetDescription(this Enum item)
        {
            if (item == null) return null;
            var attributes =
                item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof (DescriptionAttribute), false) as
                DescriptionAttribute[];
            return (attributes != null && attributes.Length > 0) ? attributes[0].Description : item.ToString();
        }
    }
     */

    /// <summary>
    ///     Extension to XmlDocument
    /// </summary>
    public static class XmlDocumentExtensions
    {
        /// <summary>
        ///     Convert the XmlDocument to XDocument
        /// </summary>
        /// <param name="xmlDocument">The source XML document.</param>
        /// <returns>
        ///     The XDocument representation of the object
        /// </returns>
        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            XDocument result;
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                result = XDocument.Load(nodeReader);
            }
            return result;
        }
    }

    /// <summary>
    ///     Extension to XElement Class
    /// </summary>
    public static class XElementExtensions
    {
        /// <summary>
        ///     Gets the decimal value from the XElement Attribute. if the element is null the default value is returned.
        /// </summary>
        /// <param name="element">The source element.</param>
        /// <param name="attributeName">Attribute name</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        ///     The <c>decimal</c> value of the attributes - Defaults to DefaultValue
        /// </returns>
        public static decimal GetDecimalAttributeValue(this XElement element, string attributeName,
                                                       decimal defaultValue = decimal.Zero)
        {
            if (element.Attribute(attributeName) == null) return defaultValue;
            decimal returnValue;
            if (!decimal.TryParse((string) element.Attribute(attributeName), out returnValue))
                returnValue = defaultValue;
            return returnValue;
        }

        /// <summary>
        ///     Gets the int value from the XElement Attribute. if the element is null the default value is returned.
        /// </summary>
        /// <param name="element">The source element.</param>
        /// <param name="attributeName">Attribute name</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        ///     The <c>int</c> value of the attributes - Defaults to DefaultValue
        /// </returns>
        public static int GetIntAttributeValue(this XElement element, string attributeName, int defaultValue = 0)
        {
            int? returnValue = GetNullableIntAttributeValue(element, attributeName);
            return returnValue != null ? returnValue.Value : defaultValue;
        }

        public static int? GetNullableIntAttributeValue(this XElement element, string attributeName,
                                                        int? defaultValue = null)
        {
            if (element.Attribute(attributeName) == null) return defaultValue;
            int returnValue;
            return int.TryParse((string) element.Attribute(attributeName), out returnValue) ? returnValue : defaultValue;
        }

        /// <summary>
        ///     Gets the bool value from the XElement Attribute. if the element is null the default value is returned.
        /// </summary>
        /// <param name="element">The source element.</param>
        /// <param name="attributeName">Attribute name</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        ///     The <c>bool</c> value of the attributes - Defaults to DefaultValue
        /// </returns>
        public static bool GetBooleanAttributeValue(this XElement element, string attributeName,
                                                    bool defaultValue = false)
        {
            if (element.Attribute(attributeName) == null) return defaultValue;
            bool returnValue;
            if (!bool.TryParse((string) element.Attribute(attributeName), out returnValue))
                returnValue = defaultValue;
            return returnValue;
        }

        public static string GetStringAttributeValue(this XElement element, string attributeName,
                                                     string defaultValue = null)
        {
            if (element.Attribute(attributeName) == null) return defaultValue;
            var returnValue = (string) element.Attribute(attributeName);
            return string.IsNullOrWhiteSpace(returnValue) ? defaultValue : returnValue;
        }
    }

    /// <summary>
    ///     Extension for Linq IEnumerable
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        ///     Traverses the specified source and returns the flat list of certain collections from the Nested Source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="fnRecurse">The fn recurse.</param>
        /// <returns>
        ///     A collection  of objects from the source
        /// </returns>
        public static IEnumerable<T> Traverse<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> fnRecurse)
        {
            if (source != null)
                foreach (T item in source)
                {
                    yield return item;
                    IEnumerable<T> seqRecurse = fnRecurse(item);
                    if (seqRecurse == null) continue;
                    foreach (T itemRecurse in Traverse(seqRecurse, fnRecurse))
                    {
                        yield return itemRecurse;
                    }
                }
        }
    }

    public static class HttpRouteDataExtensions
    {
        public static object GetValue(this IHttpRouteData source, string key, object defaultValue = null)
        {
            object result;
            if (!source.Values.TryGetValue(key, out result))
                result = defaultValue;
            return result;
        }

        public static string GetStringValue(this IHttpRouteData source, string key, string defaultValue = null)
        {
            object result = GetValue(source, key);
            if (result == null)
                return defaultValue;
            return result.ToString();
        }
    }

    /// <summary>
    ///     Extension to Object Class
    /// </summary>
    public static class ObjectExtensions
    {
        /*
        //Note: This uses 3rd party lib to deep clone a object.
        /// <summary>
        /// Deep Clones the specified obj.
        /// </summary>
        /// <typeparam name="T">Type of the object to be cloned</typeparam>
        /// <param name="obj">The source object</param>
        /// <returns></returns>
        public static T Clone<T>(this T obj)
        {
            return obj; //  (T)obj.Copy();
        }
        */

        /// <summary>
        ///     Gets the property value of the given Object.
        /// </summary>
        /// <typeparam name="T">Generic Type of the property</typeparam>
        /// <param name="obj">The source object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        ///     Returns  the value of the Object Property
        /// </returns>
        public static T GetPropertyValue<T>(this Object obj, string propertyName, T defaultValue = default(T))
        {
            obj = obj.GetPropertyValue(propertyName);
            T result = obj == null || obj.GetType() != typeof (T) ? defaultValue : (T) obj;
            return result;
        }

        /// <summary>
        ///     Gets the property value of the given Object.
        /// </summary>
        /// <param name="obj">The source object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        ///     Returns  the value of the Object Property
        /// </returns>
        public static object GetPropertyValue(this Object obj, string propertyName)
        {
            string[] parts = propertyName.Split('.');
            foreach (string part in parts)
            {
                if (obj == null) return null;
                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) return null;
                obj = info.GetValue(obj, null);
            }
            return obj;
        }
    }
}