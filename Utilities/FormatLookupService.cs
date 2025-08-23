using System;
using System.Collections.Generic;
using System.IO;

namespace Utilities
{
    public class FormatLookupService
    {
        private readonly Dictionary<string, int> _formatLookup;
        public FormatLookupService(string jsonPath)
        {
            _formatLookup = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            if (File.Exists(jsonPath))
            {
                var json = File.ReadAllText(jsonPath);
                var formatObj = System.Text.Json.JsonDocument.Parse(json);
                foreach (var format in formatObj.RootElement.GetProperty("formats").EnumerateArray())
                {
                    var name = format.GetProperty("name").GetString();
                    var id = format.GetProperty("id").GetInt32();
                    if (!string.IsNullOrEmpty(name))
                        _formatLookup[name] = id;
                }
            }
        }
        public int GetFormatId(string? displayName)
        {
            if (!string.IsNullOrEmpty(displayName) && _formatLookup.TryGetValue(displayName, out int foundId))
                return foundId;
            return 0;
        }
    }
}
