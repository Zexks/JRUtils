using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Utils.JSConverters
{
    public class DateTimeConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes =>
            new Type[] { typeof(DateTime) };

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return new JavaScriptSerializer().ConvertToType(dictionary, type);
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            if (!(obj is DateTime)) return null;
            return new JSSCustomString(((DateTime)obj).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
        }

    }
}
