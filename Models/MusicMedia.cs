using System.Collections.Generic;

namespace Models
{
    public class MusicMedia
    {
        public string? Title { get; set; }
        public string? Format { get; set; } // e.g., Vinyl, CD, Cassette
        public int Index { get; set; } // Disc number or media index
        public List<MusicTrack>? Tracks { get; set; }
    }
}
