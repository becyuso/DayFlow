using DayFlow.Modules.Notes.Domain.Notebooks;
using DayFlow.Modules.Notes.Domain.Notes;
using DayFlow.Modules.Notes.Domain.NoteTags;
using DayFlow.Modules.Notes.Domain.Tags;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Notes.Infrastructure.Database
{
    public class NoteDbContext : DbContext
    {
        public NoteDbContext(DbContextOptions<NoteDbContext> options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; } = null!;
        public DbSet<Notebook> Notebooks { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<NoteTag> NoteTags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Global Query Filters

            modelBuilder.Entity<Notebook>()
                .HasQueryFilter(
                x => !x.IsDeleted);

            modelBuilder.Entity<Note>()
                .HasQueryFilter(
                x => !x.IsDeleted);

            #endregion

            #region Entity Configurations

            modelBuilder.Entity<Notebook>(b =>
            {
                b.ToTable("notebooks", "notes");
                b.HasKey(x => x.NotebookId).HasName("pk_notebooks");
                b.Property(x => x.NotebookId).HasColumnName("notebook_id").ValueGeneratedNever();
                b.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
                b.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                b.Property(x => x.Color).HasColumnName("color").HasMaxLength(20);
                b.Property(x => x.SortOrder).HasColumnName("sort_order").HasDefaultValue(0);
                b.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("datetime2").IsRequired();
                b.Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired();
                b.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime2");
                b.Property(x => x.UpdatedBy).HasColumnName("updated_by");
                b.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
                b.Property(x => x.DeletedAt).HasColumnName("deleted_at").HasColumnType("datetime2");
                b.Property(x => x.DeletedBy).HasColumnName("deleted_by");

                //b.HasMany(x => x.Notes).WithOne(x => x.Notebook).HasForeignKey(x => x.NotebookId).HasConstraintName("fk_notes_notebooks");
            });

            modelBuilder.Entity<Note>(b =>
            {
                b.ToTable("notes", "notes");
                b.HasKey(x => x.NoteId).HasName("pk_notes");
                b.Property(x => x.NoteId).HasColumnName("note_id").ValueGeneratedNever();
                b.Property(x => x.NotebookId).HasColumnName("notebook_id").IsRequired();
                b.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
                b.Property(x => x.Title).HasColumnName("title").HasMaxLength(300).IsRequired();
                b.Property(x => x.Content).HasColumnName("content").HasMaxLength(1000);
                b.Property(x => x.Summary).HasColumnName("summary").HasMaxLength(500);
                b.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("datetime2").IsRequired();
                b.Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired();
                b.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime2");
                b.Property(x => x.UpdatedBy).HasColumnName("updated_by");
                b.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
                b.Property(x => x.DeletedAt).HasColumnName("deleted_at").HasColumnType("datetime2");
                b.Property(x => x.DeletedBy).HasColumnName("deleted_by");

                //b.HasMany(x => x.NoteTags).WithOne(x => x.Notes).HasForeignKey(x => x.NoteId).HasConstraintName("fk_note_tags_note");
            });

            modelBuilder.Entity<Tag>(b =>
            {
                b.ToTable("tags", "notes");
                b.HasKey(x => x.TagId).HasName("pk_tags");
                b.Property(x => x.TagId).HasColumnName("tag_id").ValueGeneratedNever();
                b.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
                b.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
                b.Property(x => x.Color).HasColumnName("color").HasMaxLength(20);
                b.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("datetime2").IsRequired();
                b.Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired();
                b.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime2");
                b.Property(x => x.UpdatedBy).HasColumnName("updated_by");
                b.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();
                b.Property(x => x.DeletedAt).HasColumnName("deleted_at").HasColumnType("datetime2");
                b.Property(x => x.DeletedBy).HasColumnName("deleted_by");

                //b.HasMany(x => x.NoteTags).WithOne(x => x.Tag).HasForeignKey(x => x.TagId).HasConstraintName("fk_note_tags_tag");
            });

            modelBuilder.Entity<NoteTag>(b =>
            {
                b.ToTable("note_tags", "notes");
                b.HasKey(x => new { x.NoteId, x.TagId }).HasName("pk_note_tags");
                b.Property(x => x.NoteId).HasColumnName("note_id");
                b.Property(x => x.TagId).HasColumnName("tag_id");
                b.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("datetime2").IsRequired();
                b.Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired();

            //    b.HasOne(x => x.Notes).WithMany(x => x.NoteTags).HasForeignKey(x => x.NoteId).HasConstraintName("fk_note_tags_note");
            //    b.HasOne(x => x.Tag).WithMany(x => x.NoteTags).HasForeignKey(x => x.TagId).HasConstraintName("fk_note_tags_tag");
            });

            #endregion
        }
    }
}
