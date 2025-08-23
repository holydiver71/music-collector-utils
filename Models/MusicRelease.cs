using System.Collections.Generic;


using Models;

namespace Models
{
    public class MusicRelease
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateOnly ReleaseYear { get; set; }
        public DateOnly OrigReleaseYear { get; set; }
        public List<int>? Artists { get; set; }
        public List<int>? Genres { get; set; }
        public bool Live { get; set; }
        public int LabelId { get; set; }
        public int CountryId { get; set;}
        public string? LabelNumber { get; set; }
        public string? LengthInSeconds { get; set; }
        public int FormatId { get; set; }
        // Add more fields as needed from the XML structure
        public PurchaseData? PurchaseInfo { get; set; }
        public int PackagingId { get; set; }

        public MediaImages? Images { get; set; }
        public List<LinkData>? Links { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastModified { get; set; }

        // Link to the album's media (discs, CDs, etc)
        public List<MusicMedia>? Media { get; set; }
    }
}
