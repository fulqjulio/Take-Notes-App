using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Data.Models
{
    public class NoteAndCategory : BaseEntity<int>
    {
        [ForeignKey("Note")]
        public int NoteId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

    }
}
