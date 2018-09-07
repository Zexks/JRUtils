using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Utils.JSConverters
{
    public class BigJSONConverter : JavaScriptConverter
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public string FullPath { get => System.IO.Path.Combine(Path, FileName); }

        public override IEnumerable<Type> SupportedTypes => new Type[] { typeof(Dictionary<string, string>) };

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return new JavaScriptSerializer().ConvertToType(dictionary, type);
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            if (!(obj is Dictionary<string, string>)) return null;
            var dict = (Dictionary<string, string>)obj;

            using (var sw = new StreamWriter(FullPath))
                sw.Write($"[{string.Join(",", dict.Select(kvp => $"{{\"{kvp.Key.ToString()}\":\"{kvp.Value.ToString()}\"}}").ToArray())}]");

            using (var sr = new StreamReader(FullPath))
                return new JSSCustomString(sr.ReadToEnd());
        }
    }
}
