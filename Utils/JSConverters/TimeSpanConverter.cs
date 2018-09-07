using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Utils.JSConverters
{
    public class TimeSpanConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes =>
            new Type[] { typeof(TimeSpan) };

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            Console.WriteLine(dictionary.ToString());
            return new JavaScriptSerializer().ConvertToType(dictionary, type);
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            if (!(obj is TimeSpan)) return null;
            var ts = (TimeSpan)obj;
            //return new Dictionary<string, object> { { "timespan", ts.Ticks } };
            return new JSSCustomString($"{((TimeSpan)obj).Ticks.ToString()}");

        }
    }
}
