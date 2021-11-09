using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MusicStore.Statistics
{
    public static class Reporter
    {
        private class ArtistEqualityComparer : IEqualityComparer<Artist>
        {
            public bool Equals(Artist x, Artist y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode([DisallowNull] Artist obj)
            {
                return obj.GetHashCode();
            }
        }
        public static IEnumerable<Models.Query.ArtistAlbums> QueryArtistAlbums(IEnumerable<Artist> artists, IEnumerable<Album> albums)
        {
            _ = artists ?? throw new ArgumentNullException(nameof(artists));
            _ = albums ?? throw new ArgumentNullException(nameof(albums));

            var query = artists.Select(e => new Models.Query.ArtistAlbums
            {
                Artist = e,
                Albums = albums.Where(e1 => e1.ArtistId == e.Id).ToArray(),
            });
            return query;
        }
        public static IEnumerable<Models.Query.ArtistTracks> QueryArtistTracks(IEnumerable<Artist> artists, IEnumerable<Album> albums, IEnumerable<Track> tracks)
        {
            _ = artists ?? throw new ArgumentNullException(nameof(artists));
            _ = albums ?? throw new ArgumentNullException(nameof(albums));
            _ = tracks ?? throw new ArgumentNullException(nameof(tracks));

            var artistAlbums = QueryArtistAlbums(artists, albums);
            var query = artistAlbums.Select(aa => new Models.Query.ArtistTracks
            {
                Artist = aa.Artist,
                Tracks = tracks.Where(t => aa.Albums.Any(i => i.Id == t.AlbumId)).ToArray(),
            });
            return query;
        }
    }
}
