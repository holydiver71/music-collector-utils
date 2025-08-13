using System;
using System.IO;
using MusicCollectorConsole.Utils;

class ExtractEntity
{
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: ExtractEntity <entity>");
            return;
        }

        string entity = args[0].ToLower();
        string xmlPath = "MusicCollectorzExport.xml";
        string outputDir = "data";
        string outputFile = Path.Combine(outputDir, $"{entity}s.json");

        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        var doc = XmlParser.LoadXml(xmlPath);
        var names = EntityExtractor.ExtractEntityDisplayNames(doc, entity);
        var entitiesWithIds = UniqueIdGenerator.AssignNumericIds(names);
        JsonGenerator.WriteGenericJson(entity, entitiesWithIds, outputFile);

        Console.WriteLine($"Extracted {entitiesWithIds.Count} {entity}s to {outputFile}");
    }
}
