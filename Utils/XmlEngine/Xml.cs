using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using Utils.Extensions;

namespace Utils.XmlEngine
{
    public static class Xml
    {
        public static XDocument CreateXDoc(DataTable tbl)
        {
            using (var xml = new XmlNodeReader(CreateXmlDoc(tbl)))
                return XDocument.Load(xml);
        }
        public static XmlDocument CreateXmlDoc(DataTable tbl)
        {
            using (var ms = new MemoryStream())
            {
                tbl.WriteXml(ms);
                using (var sr = new StreamReader(ms.ResetPosition()))
                    return new XmlDocument().Parse(sr.ReadToEnd());
            }
        }
    }

}
