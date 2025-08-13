using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MusicCollectorConsole.Utils
{
    public static class JsonGenerator
    {
        public static void WriteGenericJson(string entity, List<(int id, string name)> items, string filePath)
        {
            // Pluralize entity for property name (simple pluralization)
            string property = entity.EndsWith("s") ? entity + "es" : entity + "s";
            var obj = new Dictionary<string, object>
            {
                [property] = items.Select(a => new { id = a.id, name = a.name }).ToList()
            };
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(obj, options);
            File.WriteAllText(filePath, json);
        }
    }
}
