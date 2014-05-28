#region using

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Caching;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using EM.Framework.Cache;

#endregion

namespace EM.Framework.Utilities
{
    public class XsltTransformer
    {
        private readonly XsltArgumentList _args;

        private readonly ICacheStorage _cache;

        public XsltTransformer(ICacheStorage cacheStorage)
        {
            _args = new XsltArgumentList();
            _cache = cacheStorage;
        }

        public void AddParameter(string name, string value, string namespaceUri = "")
        {
            _args.AddParam(name, namespaceUri, value);
        }

        public void AddExtensionObject(string name, object value)
        {
            _args.AddExtensionObject(string.Format(CultureInfo.InvariantCulture, "urn:{0}", name), value);
        }

        public string Transform(XNode xmlData, string xsltFilePath)
        {
            string result;
            using (var writer = new StringWriter(CultureInfo.InvariantCulture))
            {
                Transform(xmlData, xsltFilePath, writer);
                result = writer.ToString();
            }
            return result;
        }

        public void Transform(string xmlFilePath, string xsltFilePath, string outFilePath = null,
            string encoding = TextEncoding.Utf8)
        {
            string outputFilePath = string.IsNullOrWhiteSpace(outFilePath) ? xmlFilePath : outFilePath;
            if (xmlFilePath == null || outputFilePath == null)
                return;
            XDocument xDoc = XDocument.Load(xmlFilePath);
            using (var writer = new StreamWriter(outputFilePath, false, TextEncoding.GetTextEncoding(encoding)))
            {
                Transform(xDoc, xsltFilePath, writer);
            }
        }

        public void Transform(XNode xmlData, string xsltFilePath, TextWriter writer)
        {
            if (xmlData == null) return;
            XslCompiledTransform xslTransform = GetCompiledXslTransform(xsltFilePath);
            using (XmlReader xmlReader = xmlData.CreateReader())
            {
                xslTransform.Transform(xmlReader, _args, writer);
            }
        }

        protected XslCompiledTransform GetCompiledXslTransform(string xsltFilePath)
        {
            //Create CacheItem Policy;
            
            XslCompiledTransform xslCompiledTransform;
            if (_cache == null)
            {
                xslCompiledTransform = GetXslTransform(xsltFilePath);
            }
            else
            {
                var policy = new CacheItemPolicy();
                var paths = new List<string> { xsltFilePath };
                policy.ChangeMonitors.Add(new HostFileChangeMonitor(paths));
                policy.SlidingExpiration = CacheMgr.DefaultCacheTimeSpan;

                xslCompiledTransform = _cache.AddOrGetExisting(string.Concat("xslt_", xsltFilePath),
                () => GetXslTransform(xsltFilePath), policy);
            }

            return xslCompiledTransform;
        }

        protected XslCompiledTransform GetXslTransform(string xsltFilePath)
        {
            var xslTransform = new XslCompiledTransform();
            try
            {
                xslTransform.Load(xsltFilePath);
            }
            catch (Exception ex)
            {
                throw new
                    Exception(string.Format(CultureInfo.InvariantCulture,
                        "Error encountered while compiling style sheet {0} : Exception: {1}",
                        xsltFilePath, ex));
            }
            return xslTransform;
        }
    }
}