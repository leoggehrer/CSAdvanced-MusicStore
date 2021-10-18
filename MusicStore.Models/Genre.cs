namespace MusicStore.Models
{
	public class Genre : ModelObject
	{
		public string Name { get; set; }
		public override string ToString() => $"{Name} - [{Id}]";
	}
}
