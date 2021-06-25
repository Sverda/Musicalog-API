using Musicalog.Domain.Enums;

namespace Musicalog.Domain.Entities
{
    public class Album
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ArtistId { get; set; }

        public Artist Artist { get; set; }

        public AlbumType Type { get; set; }

        public int Stock { get; set; }
    }
}
