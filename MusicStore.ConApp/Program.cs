using MusicStore.Statistics;
using System;

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
		}
	}
}
