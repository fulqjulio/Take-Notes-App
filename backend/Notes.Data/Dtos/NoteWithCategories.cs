using Notes.Data.Models;

namespace Notes.Data.Dtos
{
    public class NoteWithCategories
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public virtual IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();
    }
}
