using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicStore.Statistics
{
	public static class Loader
	{
		public static IEnumerable<Models.Album> CreateObjectModel(IEnumerable<Models.Album> albums, IEnumerable<Models.Artist> artists, IEnumerable<Models.Track> tracks, IEnumerable<Models.Genre> genres)
		{
			var result = new List<Models.Album>();

			albums.ToList().ForEach(a =>
			{
				a.Artist = artists.FirstOrDefault(art => art.Id == a.ArtistId);
				a.Tracks = tracks.Where(t => t.AlbumId == a.Id)
								.Select(t =>
								{
									t.Album = a;
									t.Genre = genres.FirstOrDefault(g => g.Id == t.GenreId);
									return t;
								})
								 .ToList();
				result.Add(a);
			});
			return result;
		}
		public static IEnumerable<Models.Artist> LoadArtists(string filePath)
		{
			return File.ReadAllLines(filePath, encoding: System.Text.Encoding.Default)
					   .Skip(1)                     // IEnumerable<string>
					   .Select(l => l.Split(";"))   // IEnumerable<string[]>
					   .Select(d => new Models.Artist
					   {
						   Id = Convert.ToInt32(d[0]),
						   Name = d[1],
					   });
		}
		public static IEnumerable<Models.Album> LoadAlbums(string filePath)
		{
			return File.ReadAllLines(filePath, encoding: System.Text.Encoding.Default)
					   .Skip(1)                     // IEnumerable<string>
					   .Select(l => l.Split(";"))   // IEnumerable<string[]>
					   .Select(d => new Models.Album
					   {
						   Id = Convert.ToInt32(d[0]),
						   Title = d[1],
						   ArtistId = Convert.ToInt32(d[2]),
					   });
		}
		public static IEnumerable<Models.Genre> LoadGenres(string filePath)
		{
			return File.ReadAllLines(filePath, encoding: System.Text.Encoding.Default)
					   .Skip(1)                     // IEnumerable<string>
					   .Select(l => l.Split(";"))   // IEnumerable<string[]>
					   .Select(d => new Models.Genre
					   {
						   Id = Convert.ToInt32(d[0]),
						   Name = d[1],
					   });
		}
		public static IEnumerable<Models.Track> LoadTracks(string filePath)
		{
			return File.ReadAllLines(filePath, encoding: System.Text.Encoding.Default)
					   .Skip(1)                     // IEnumerable<string>
					   .Select(l => l.Split(";"))   // IEnumerable<string[]>
					   .Select(d => new Models.Track
					   {
						   Id = Convert.ToInt32(d[0]),
						   Name = d[1],
						   AlbumId = Convert.ToInt32(d[2]),
						   GenreId = Convert.ToInt32(d[4]),
						   Milliseconds = Convert.ToInt64(d[6]),
						   UnitPrice = Convert.ToDecimal(d[8])
					   });
		}
	}
}
