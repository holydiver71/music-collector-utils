using System.Collections.Generic;

namespace Models
{
    public class MusicTrack
    {
        public int Id { get; set; }
         public string? Title { get; set; }
        public string? ReleaseYear { get; set; }
        public List<string>? Artists { get; set; }
        public List<string>? Genres { get; set; }
        public bool Live { get; set; }
        public int LengthSecs { get; set; }
        public int Position { get; set; }
        // Add more fields as needed from the XML structure
    }
}
