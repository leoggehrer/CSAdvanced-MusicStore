
namespace MusicStore.Models
{
	public class Track : ModelObject
	{
		public int AlbumId { get; set; }
		public string Name { get; set; }
		public int GenreId { get; set; }
		public string Composer { get; set; }
		public long Milliseconds { get; set; }
		public long Bytes { get; set; }
		public decimal UnitPrice { get; set; }
		// Navigation Properties
		public Album Album { get; set; }
		public Genre Genre { get; set; }
		public override string ToString() => $"{Name} - [{Id}]";
	}
}
