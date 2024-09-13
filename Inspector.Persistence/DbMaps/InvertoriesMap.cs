using Inspector.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inspector.Persistence.DbMaps
{
    public class InvertoriesMap : IEntityTypeConfiguration<InvertoriesDb>
    {
        public void Configure(EntityTypeBuilder<InvertoriesDb> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnName("Наименование");
            builder.Property(e => e.Number).HasColumnName("Инв. Номер");
            builder.HasIndex(e => e.Number).IsUnique();
            builder.Property(e => e.ForDestruction).HasColumnName("На уничтожение");
            builder.Property(e => e.DestructionMark).HasColumnName("Отметка об уничтожении");
            builder.Property(e => e.Note).HasColumnName("Примечание");
            builder.Property(e => e.CreateDate).HasColumnName("CreateDate");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            builder.Property(e => e.UpdateDate).HasColumnName("UpdateDate");
            builder.Property(e => e.UpdatedBy).HasColumnName("UpdatedBy");

            builder.HasMany(v => v.DocumentRaspOVVDb).WithOne(d => d.InventoryDb).HasForeignKey(d => d.InventoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(v => v.DocumentActReportDb).WithOne(d => d.InventoryDb).HasForeignKey(d => d.InventoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(v => v.DocumentThirdDb).WithOne(d => d.InventoryDb).HasForeignKey(d => d.InventoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(v => v.DocumentFirstDb).WithOne(d => d.InventoryDb).HasForeignKey(d => d.InventoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(v => v.DocumentSecondDb).WithOne(d => d.InventoryDb).HasForeignKey(d => d.InventoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(i => i.SertificatesDb).WithOne(s => s.InventoryDb).HasForeignKey(s => s.InventoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(i => i.DocumentsOthersDb).WithOne(s => s.InventoryDb).HasForeignKey(s => s.InventoryId).OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Инвентари");

        }
    }
}
