using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Extensions
{
    public static class Misc
    {
        public static SecureString Load(this SecureString ss, string input)
        {
            input.ForEach(c => ss.AppendChar(c));
            return ss;
        }
    }
}
