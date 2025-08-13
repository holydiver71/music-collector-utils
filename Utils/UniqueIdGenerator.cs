namespace MusicCollectorConsole.Utils
{
    public static class UniqueIdGenerator
    {
        public static List<(int id, string name)> AssignNumericIds(List<string> names)
        {
            var result = new List<(int, string)>();
            int id = 1;
            foreach (var name in names)
            {
                result.Add((id++, name));
            }
            return result;
        }
    }
}
