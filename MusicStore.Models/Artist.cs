namespace MusicStore.Models
{
	public class Artist : ModelObject
	{
		public string Name { get; set; }
		public override string ToString() => $"{Name} - [{Id}]";
	}
}
