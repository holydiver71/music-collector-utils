using System;
using System.Collections.Generic;
using System.IO;

namespace Utilities
{
    public class LabelLookupService
    {
        private readonly Dictionary<string, int> _labelLookup;
        public LabelLookupService(string jsonPath)
        {
            _labelLookup = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            if (File.Exists(jsonPath))
            {
                var json = File.ReadAllText(jsonPath);
                var labelObj = System.Text.Json.JsonDocument.Parse(json);
                int id = 1;
                foreach (var label in labelObj.RootElement.GetProperty("labels").EnumerateArray())
                {
                    var name = label.GetProperty("name").GetString();
                    if (!string.IsNullOrEmpty(name) && !_labelLookup.ContainsKey(name))
                        _labelLookup[name] = id++;
                }
            }
        }
        public int GetLabelId(string? displayName)
        {
            if (!string.IsNullOrEmpty(displayName) && _labelLookup.TryGetValue(displayName, out int foundId))
                return foundId;
            return 0;
        }
    }
}
