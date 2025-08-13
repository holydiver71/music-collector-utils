using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MusicCollectorConsole.Utils
{
    public static class EntityExtractor
    {
        public static List<string> ExtractEntityDisplayNames(XDocument doc, string entityName)
        {
            return doc.Descendants(entityName)
                .Elements("displayname")
                .Select(e => e.Value.Trim())
                .Where(name => !string.IsNullOrEmpty(name))
                .Distinct()
                .OrderBy(name => name)
                .ToList();
        }
    }
}
