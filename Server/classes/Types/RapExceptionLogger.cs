#region Using

using System;
using System.Web;
using System.Xml;

#endregion

namespace FreestyleOnline.classes.Types
{
    public static class RapExceptionLogger
    {
        private static XmlDocument _document;
        private static XmlElement _rootElement;
        private static XmlElement _element;
        private static XmlElement _node;

        /// <summary>
        ///     Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public static void Log(Exception exception)
        {
            _document = new XmlDocument();
            _document.Load(HttpContext.Current.Server.MapPath("~/Resources/Exceptions.xml"));
            _node = _document.DocumentElement;
            _rootElement = _document.CreateElement("Exception");
            _node.AppendChild(_rootElement);
            _element = _document.CreateElement("Message");
            _element.InnerText = exception.InnerException != null ? exception.InnerException.Message : exception.Message;
            _rootElement.AppendChild(_element);
            _element = _document.CreateElement("Source");
            _element.InnerText = exception.Source;
            _rootElement.AppendChild(_element);
            _element = _document.CreateElement("Stack");
            _element.InnerText = exception.StackTrace;
            _rootElement.AppendChild(_element);
            _element = _document.CreateElement("Date");
            _element.InnerText = DateTime.Now.ToShortDateString();
            _rootElement.AppendChild(_element);
            _document.Save(HttpContext.Current.Server.MapPath("~/Resources/Exceptions.xml"));
        }
    }
}