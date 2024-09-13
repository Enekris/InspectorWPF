using Inspector.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inspector.Persistence.DbMaps
{
    public class HardwareFilterNameMap : IEntityTypeConfiguration<HardwareFilterNameDb>
    {
        public void Configure(EntityTypeBuilder<HardwareFilterNameDb> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnName("Наименование");
            builder.HasIndex(e => e.Name).IsUnique();
            builder.Property(e => e.CreateDate).HasColumnName("CreateDate");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            builder.Property(e => e.UpdateDate).HasColumnName("UpdateDate");
            builder.Property(e => e.UpdatedBy).HasColumnName("UpdatedBy");

            builder.HasMany(f => f.HardwaresDb).WithOne(h => h.FilterDb).HasForeignKey(h => h.NameId).OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Наим. ТС");

        }
    }
}
