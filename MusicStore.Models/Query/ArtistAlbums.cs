using System.Collections.Generic;

namespace MusicStore.Models.Query
{
    public class ArtistAlbums
    {
        public Artist Artist { get; init; }
        public IEnumerable<Album> Albums { get; init; }
    }
}
