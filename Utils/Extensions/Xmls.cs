using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Utils.Extensions
{
    public static class XmlExtensions
    {

        #region XmlDocument
        public static XmlDocument Parse(this XmlDocument doc, string xml)
        {
            doc.LoadXml(xml);
            return doc;
        }

        public static XmlNode AddNode(this XmlNode node, XmlNodeType type, string name) =>
            node.AddNode(node.OwnerDocument.CreateNode(type, name, string.Empty));
        public static XmlNode AddNode(this XmlNode node, XmlNode nn)
        { node.AppendChild(nn); return node; }

        public static XmlNode AddElement(this XmlNode node, string name) =>
            node.AddElement(node.OwnerDocument.CreateElement(name));
        public static XmlNode AddElement(this XmlNode node, XmlElement e)
        { node.AppendChild(e); return node; }

        public static XmlNode AddAttribute(this XmlNode node, string name, string value) =>
            node.AddAttribute(node.OwnerDocument.CreateAttribute(name).SetValue(value));
        public static XmlNode AddAttribute(this XmlNode node, XmlAttribute a)
        { node.Attributes.Append(a); return node; }

        public static XmlAttribute SetValue(this XmlAttribute att, string value)
        { att.Value = value; return att; }

        #endregion

        #region XDocument
        public static XElement AddElement(this XElement e, string newE) =>
            e.AddElement(new XElement(newE));
        public static XElement AddElement(this XElement e, XElement newE)
        { e.Add(newE); return e; }

        public static XElement AddAttribute(this XElement e, string name, string val) =>
            e.AddAttribute(new XAttribute(name, val));
        public static XElement AddAttribute(this XElement e, XAttribute att)
        { e.Add(att); return e; }

        public static XElement SetValue(this XElement e, object val)
        { e.Value = val.ToString(); return e; }

        #endregion
    }
}
