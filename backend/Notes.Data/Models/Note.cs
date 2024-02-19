namespace Notes.Data.Models
{
	public class Note : BaseEntity<int>
	{
		public string? Title { get; set; }
		public string? Content { get; set; }
		public bool IsArchived { get; set; } = false;
		public DateTime CreatedTime { get; set; } 
		public DateTime LastUpdatedTime { get; set; }

    }
}
