using System;

namespace CelebsAPI.Models
{
	[Serializable]
	public class Celebrity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Page { get; set; }
		public string Occupation { get; set; }
		public string Image { get; set; }
		public string Birth { get; set; }
	}
}