#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using FreestyleOnline.classes.Base;

#endregion

namespace FreestyleOnline.classes.Types.Helpers
{
    public class XmlExceptionParser : RapClass
    {
        #region Properties

        public string Message { get; set; }
        public string Source { get; set; }
        public string Stack { get; set; }
        public DateTime Date { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Reads this instance.
        /// </summary>
        public List<XmlExceptionParser> Read(string exceptionPath)
        {
            var exceptionsList = new List<XmlExceptionParser>();
            var xmlText = new XmlTextReader(exceptionPath);
            while (xmlText.Read())
            {
                var xmlNode = new XmlExceptionParser();
                if (xmlText.MoveToContent() == XmlNodeType.Element && xmlText.Name == "Message")
                {
                    xmlNode.Message = xmlText.ReadElementString();
                }
                if (xmlText.MoveToContent() == XmlNodeType.Element && xmlText.Name == "Source")
                {
                    xmlNode.Source = xmlText.ReadElementString();
                }
                if (xmlText.MoveToContent() == XmlNodeType.Element && xmlText.Name == "Stack")
                {
                    xmlNode.Stack = xmlText.ReadElementString();
                }
                if (xmlText.MoveToContent() == XmlNodeType.Element && xmlText.Name == "Date")
                {
                    xmlNode.Date = Convert.ToDateTime(xmlText.ReadElementString());
                }
                if (!string.IsNullOrEmpty(xmlNode.Message) && !string.IsNullOrEmpty(xmlNode.Source) &&
                    !string.IsNullOrEmpty(xmlNode.Stack))
                {
                    exceptionsList.Add(xmlNode);
                }
            }
            xmlText.Close();
            return exceptionsList;
        }

        /// <summary>
        ///     Deletes this instance.
        /// </summary>
        /// <param name="xmlContentToDelete">The XML content to delete.</param>
        /// <param name="exceptionPath">The exception path.</param>
        public void Delete(string xmlContentToDelete, string exceptionPath)
        {
            var doc = new XmlDocument();
            doc.Load(exceptionPath);
            var exceptions = doc.SelectSingleNode("//Exceptions");
            foreach (XmlNode exception in exceptions.ChildNodes)
            {
                foreach (
                    var child in
                        exception.ChildNodes.Cast<XmlNode>()
                            .Where(child => child.Name == "Message" && child.InnerXml == xmlContentToDelete))
                {
                    child.ParentNode.RemoveAll();
                    break;
                }
            }
            doc.Save(exceptionPath);
        }

        #endregion
    }
}