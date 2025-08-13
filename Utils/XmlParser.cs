using System.Xml.Linq;

namespace MusicCollectorConsole.Utils
{
    public static class XmlParser
    {
        public static XDocument LoadXml(string filePath)
        {
            return XDocument.Load(filePath);
        }
    }
}
