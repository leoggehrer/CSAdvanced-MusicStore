using MusicStore.Models;
using MusicStore.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.ConApp
{
    internal class Program
    {
        private static void Main(/*string[] args*/)
        {
            Console.WriteLine("MusicStore - Statistics");
            var artists = Loader.LoadArtists(@"Data\Artist.csv");
            var albums = Loader.LoadAlbums(@"Data\Album.csv");
            var tracks = Loader.LoadTracks(@"Data\Track.csv");
            var genres = Loader.LoadGenres(@"Data\Genre.csv");
            var albumModels = Loader.CreateObjectModel(albums, artists, tracks, genres);

            //PrintArtistAndAlbums(artists, albums);
            //PrintArtistAndTracks(artists, albums, tracks);
            //PrintArtistAndSongTime(artists, albums, tracks);
            //PrintAlbumAndSongs(albumModels);
            //PrintAlbumAndTime(albumModels);
            //PrintGenreAndTimes(genres, tracks);
            //PrintAlbumAndSongTime(albums, tracks);
            //PrintTrackAndSongTime(tracks);
            PrintGenreAndCount(genres, tracks);
        }

        /// <summary>
        /// Prints the artists and their albums on the console. 
        /// </summary>
        /// <param name="artists">The collection of artists</param>
        /// <param name="albums">The collection of albums</param>
        private static void PrintArtistAndAlbums(IEnumerable<Artist> artists, IEnumerable<Album> albums)
        {
            Console.WriteLine("Artist -> Albums");
            Console.WriteLine("================");
            Reporter.QueryArtistAlbums(artists, albums)
                    .OrderBy(e => e.Artist.Name)
                    .ToList()
                    .ForEach(e =>
                    {
                        Console.WriteLine($"Artist: {e.Artist.Name}");
                        e.Albums.ToList()
                                .ForEach(e1 =>
                                {
                                    Console.WriteLine($"\tAlbum: {e1.Title}");
                                });
                    });
        }
        /// <summary>
        /// Prints the artists and their tracks on the console. 
        /// </summary>
        /// <param name="artists">The collection of artists</param>
        /// <param name="albums">The collection of albums</param>
        /// <param name="tracks">The collection of tracks</param>
        private static void PrintArtistAndTracks(IEnumerable<Artist> artists, IEnumerable<Album> albums, IEnumerable<Track> tracks)
        {
            Console.WriteLine("Artist -> Tracks");
            Console.WriteLine("================");
            Reporter.QueryArtistTracks(artists, albums, tracks)
                    .OrderBy(e => e.Artist.Name)
                    .ToList()
                    .ForEach(e =>
                    {
                        Console.WriteLine($"Artist: {e.Artist.Name}");
                        e.Tracks.ToList()
                                .ForEach(e1 =>
                                {
                                    Console.WriteLine($"\tTrack: {e1.Name}");
                                });
                    });
        }
        /// <summary>
        /// Prints the artists and the track times in [sec] on the console. 
        /// </summary>
        /// <param name="artists">The collection of artists</param>
        /// <param name="albums">The collection of albums</param>
        /// <param name="tracks">The collection of tracks</param>
        private static void PrintArtistAndSongTime(IEnumerable<Artist> artists, IEnumerable<Album> albums, IEnumerable<Track> tracks)
        {
            Console.WriteLine("Artist -> SongTime [sec]");
            Console.WriteLine("========================");
            Reporter.QueryArtistTracks(artists, albums, tracks)
                    .OrderBy(e => e.Artist.Name)
                    .ToList()
                    .ForEach(e =>
                    {
                        Console.WriteLine($"Artist: {e.Artist.Name}");
                        Console.WriteLine($"\tSongtime sum [sec]: {e.Tracks.Select(t => t.Milliseconds).Sum() / 1000:f}");
                        Console.WriteLine($"\tSongtime avg [sec]: {e.Tracks.Select(t => t.Milliseconds).DefaultIfEmpty(0).Average() / 1000:f}");
                    });
        }
        /// <summary>
        /// Prints the album and the their tracks on the console. 
        /// </summary>
        /// <param name="albumModels">The full collection of albums</param>
        private static void PrintAlbumAndSongs(IEnumerable<Album> albumModels)
        {
            Console.WriteLine("Album -> Songname");
            Console.WriteLine("=================");

            albumModels.ToList()
                .ForEach(am =>
                    {
                        Console.WriteLine($"Album: {am.Title} ({am.Artist.Name})");
                        am.Tracks.ToList()
                          .ForEach(t =>
                          {
                              Console.WriteLine($"\tSong: {t.Name}");
                          });
                    });
        }
        /// <summary>
        /// Prints the album and the album time in [sec] on the console. 
        /// </summary>
        /// <param name="albumModels">The full collection of albums</param>
        private static void PrintAlbumAndTime(IEnumerable<Album> albumModels)
        {
            Console.WriteLine("Album -> Time [sec]");
            Console.WriteLine("========================");

            albumModels.ToList()
                .ForEach(am =>
                {
                    Console.WriteLine($"Album: {am.Title} ({am.Artist.Name})");
                    Console.WriteLine($"\tTime: {am.Tracks.Select(t => t.Milliseconds).Sum() / 1000:f}");
                });
        }
        /// <summary>
        /// Prints the genre and the average track time in [sec] on the console. 
        /// </summary>
        /// <param name="genres">The collection of genres</param>
        /// <param name="tracks">The collection of tracks</param>
        private static void PrintGenreAndTimes(IEnumerable<Genre> genres, IEnumerable<Track> tracks)
        {
            Console.WriteLine("Genre -> Time [sec]");
            Console.WriteLine("========================");

            genres.ToList()
                  .ForEach(g =>
                  {
                      Console.WriteLine($"Genre: {g.Name}");
                      Console.WriteLine($"\tTime: {tracks.Where(t => t.GenreId == g.Id).Select(t => t.Milliseconds).DefaultIfEmpty(0).Average() / 1000:f}");
                  });
        }
        /// <summary>
        /// Prints the album and the track times in [sec] on the console. 
        /// </summary>
        /// <param name="albums">The collection of albums</param>
        /// <param name="tracks">The collection of tracks</param>
        private static void PrintAlbumAndSongTime(IEnumerable<Album> albums, IEnumerable<Track> tracks)
        {
            Console.WriteLine("Album -> SongTime [sec]");
            Console.WriteLine("========================");
            albums.ToList()
                  .ForEach(a =>
                    {
                        Console.WriteLine($"Album: {a.Title}");
                        Console.WriteLine($"\tSongtime sum [sec]: {tracks.Where(t => t.AlbumId == a.Id).Select(t => t.Milliseconds).Sum() / 1000:f}");
                        Console.WriteLine($"\tSongtime avg [sec]: {tracks.Where(t => t.AlbumId == a.Id).Select(t => t.Milliseconds).DefaultIfEmpty(0).Average() / 1000:f}");
                    });
        }
        /// <summary>
        /// Prints the track and the times in [sec] on the console. 
        /// </summary>
        /// <param name="tracks">The collection of tracks</param>
        private static void PrintTrackAndSongTime(IEnumerable<Track> tracks)
        {
            Console.WriteLine("Track -> SongTime [sec]");
            Console.WriteLine("========================");

            Console.WriteLine($"\tSongtime sum [sec]: {tracks.Select(t => t.Milliseconds).Sum() / 1000:f}");
            Console.WriteLine($"\tSongtime avg [sec]: {tracks.Select(t => t.Milliseconds).DefaultIfEmpty(0).Average() / 1000:f}");
        }
        /// <summary>
        /// Prints the genre and the count on the console. 
        /// </summary>
        /// <param name="genres">The collection of genres</param>
        /// <param name="tracks">The collection of tracks</param>
        private static void PrintGenreAndCount(IEnumerable<Genre> genres, IEnumerable<Track> tracks)
        {
            Console.WriteLine("Genre -> Count");
            Console.WriteLine("===============");

            genres.ToList()
                  .ForEach(g =>
                  {
                      Console.WriteLine($"Genre: {g.Name}");
                      Console.WriteLine($"\tTime: {tracks.Where(t => t.GenreId == g.Id).Count()}");
                  });
        }
    }
}
