#region using

using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

#endregion

namespace EM.Framework.Utilities
{
    /// <summary>
    ///     Helper class to Validate Xml Document for a Given Schema ( DTD/XSD)
    /// </summary>
    public class XmlValidator
    {
        private XmlValidationResponse _validationResponse;

        internal XmlValidator()
        {
            _validationResponse = new XmlValidationResponse();
        }

        /// <summary>
        ///     Validates the specified XML file against the XSD.
        /// </summary>
        /// <param name="xmlFilePath">The XML file path.</param>
        /// <param name="xsdFilePath">The XSD file path.</param>
        /// <returns>
        ///     Returns object of <see cref="XmlValidationResponse" /> type.
        /// </returns>
        public static XmlValidationResponse Validate(string xmlFilePath, string xsdFilePath)
        {
            XDocument xDocument = XDocument.Load(xmlFilePath);
            return Validate(xDocument, xsdFilePath);
        }

        /// <summary>
        ///     Validates the specified XML document against the XSD.
        /// </summary>
        /// <param name="document">Xml document.</param>
        /// <param name="xsdFilePath">The XSD file path.</param>
        /// <returns>
        ///     Returns object of <see cref="XmlValidationResponse" /> type.
        /// </returns>
        public static XmlValidationResponse Validate(XDocument document, string xsdFilePath)
        {
            var validator = new XmlValidator();
            return validator.ValidateXmlByXsd(document, xsdFilePath);
        }

        /// <summary>
        ///     Validates the specified XML document against a Schema - supports both XSD/DTD.
        /// </summary>
        /// <param name="xmlFilePath">The XML file path.</param>
        /// <param name="schemaFilePath">The schema file path.</param>
        /// <param name="validationType">Type of the validation.</param>
        /// <returns>
        ///     Returns object of <see cref="XmlValidationResponse" /> type.
        /// </returns>
        public static XmlValidationResponse ValidateBySchema(string xmlFilePath, string schemaFilePath,
                                                             ValidationType validationType = ValidationType.DTD)
        {
            var validator = new XmlValidator();
            return validator.ValidateXmlBySchema(xmlFilePath, schemaFilePath, validationType);
        }

        /// <summary>
        ///     Validates the specified XML document against a Schema - supports both XSD/DTD.
        /// </summary>
        /// <param name="xmlFilePath">The XML file path.</param>
        /// <param name="schemaFilePath">The schema file path.</param>
        /// <param name="validationType">Type of the validation.</param>
        /// <returns>
        ///     Returns object of <see cref="XmlValidationResponse" /> type.
        /// </returns>
        private XmlValidationResponse ValidateXmlBySchema(string xmlFilePath, string schemaFilePath,
                                                          ValidationType validationType)
        {
            _validationResponse = new XmlValidationResponse();
            using (var xmlTextReader = new XmlTextReader(xmlFilePath))
            {
                using (var schemaReader = new XmlTextReader(schemaFilePath))
                {
                    var readerSettings = new XmlReaderSettings {ValidationType = validationType};
                    readerSettings.Schemas.Add(null, schemaReader);
                    readerSettings.ValidationEventHandler += XmlValidationHandler;

                    using (XmlReader objXmlReader = XmlReader.Create(xmlTextReader, readerSettings))
                    {
                        while (objXmlReader.Read())
                        {
                        }
                    }
                }
            }
            return _validationResponse;
        }

        /// <summary>
        ///     Validates the specified XML document against a XSD.
        /// </summary>
        /// <param name="xDocument">Xml Document.</param>
        /// <param name="xsdFilePath">The XSD file path.</param>
        /// <param name="targetNamespace">The target namespace.</param>
        /// <returns>
        ///     Returns object of <see cref="XmlValidationResponse" /> type.
        /// </returns>
        private XmlValidationResponse ValidateXmlByXsd(XDocument xDocument, string xsdFilePath,
                                                       string targetNamespace = "")
        {
            _validationResponse = new XmlValidationResponse();
            var schemaSet = new XmlSchemaSet();
            schemaSet.Add(targetNamespace, xsdFilePath);
            xDocument.Validate(schemaSet, XmlValidationHandler);
            return _validationResponse;
        }

        /// <summary>
        ///     Validation event handler. This method will be called for each error encountered in XML Validation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">
        ///     The <see cref="System.Xml.Schema.ValidationEventArgs" /> instance containing the event data.
        /// </param>
        private void XmlValidationHandler(object sender, ValidationEventArgs args)
        {
            _validationResponse.ErrorMessage.Add(args.Message);
        }
    }


    /// <summary>
    ///     The XML Validation Response Object is used to carry the result of the XML Validation process.
    ///     This response object contains all the information about the errors if any.
    /// </summary>
    public class XmlValidationResponse
    {
        public XmlValidationResponse()
        {
            ErrorMessage = new Collection<string>();
        }

        public int ErrorCount
        {
            get { return ErrorMessage.Count; }
        }

        public Collection<string> ErrorMessage { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether the XML is valid against the given schema.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get { return ErrorMessage.Count == 0; }
        }
    }
}