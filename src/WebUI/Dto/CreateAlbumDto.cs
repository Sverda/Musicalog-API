namespace Musicalog.WebUI.Dto
{
    public class CreateAlbumDto
    {
        public string Title { get; set; }

        public int ArtistId { get; set; }

        public string AlbumType { get; set; }

        public int Stock { get; set; }
    }
}
