using System.Collections.Generic;

namespace MusicStore.Models.Query
{
    public class ArtistTracks
    {
        public Artist Artist { get; init; }
        public IEnumerable<Track> Tracks { get; init; }
    }
}
