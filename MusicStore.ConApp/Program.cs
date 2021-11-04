using MusicStore.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.ConApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("MusicStore - Statistics");
			var artists = Loader.LoadArtists(@"Data\Artist.csv");
			var albums = Loader.LoadAlbums(@"Data\Album.csv");
			var tracks = Loader.LoadTracks(@"Data\Track.csv");
			var genres = Loader.LoadGenres(@"Data\Genre.csv");
			var albumModels = Loader.CreateObjectModel(albums, artists, tracks, genres);

			PrintArtistAndAlbums(albumModels);
		}

		static void PrintArtistAndAlbums(IEnumerable<Models.Album> albums)
		{
			var query = albums.GroupBy(al => al.Artist.Name, l => l.Title);

			query.ToList().ForEach(e =>
			{
				var count = e.Count();

				Console.WriteLine($"Artist: {e.Key}");
				Console.WriteLine($"\tAlbums: {count}");
			});
		}
	}
}
