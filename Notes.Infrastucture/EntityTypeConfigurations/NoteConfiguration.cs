using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Domain.Models;

namespace Notes.Infrastructure.EntityTypeConfigurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Title);

            builder.Property(x => x.Description).HasMaxLength(100);

            builder.Property(x => x.IsDone).IsRequired();

        }
    }
}
