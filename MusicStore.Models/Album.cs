using System.Collections.Generic;

namespace MusicStore.Models
{
	public class Album : ModelObject
	{
		public int ArtistId { get; set; }
		public string Title { get; set; }

		// Navigation Property
		public Artist Artist { get; set; }
		public IEnumerable<Track> Tracks { get; set; }
		public override string ToString() => $"{Title} - [{Id}]";
	}
}
