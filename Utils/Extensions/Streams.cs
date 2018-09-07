using System.IO;

namespace Utils.Extensions
{
    public static class StreamExtensions
    {
        public static Stream ResetPosition(this Stream s)
        {
            s.Position = 0;
            return s;
        }
    }
}
