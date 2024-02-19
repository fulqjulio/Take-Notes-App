using Notes.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Notes.Data
{
    public class NotesDBContext : DbContext
    {
        public NotesDBContext(DbContextOptions<NotesDBContext> options) : base(options)
        {
        }

        public DbSet<Note> Note { get; set; } = null!;
        public DbSet<NoteAndCategory> NoteAndCategory { get; set; } = null!;
        public DbSet<Category> Category { get; set; } = null!;

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>()
                .HasMany(p => p.Id)
                .WithOne(s => s.Note)
                .HasForeignKey(s => s.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }*/
    }
}