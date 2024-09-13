using Inspector.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inspector.Persistence.DbMaps
{
    public class DocumentsFirstMap : IEntityTypeConfiguration<DocumentsFirstDb>
    {
        public void Configure(EntityTypeBuilder<DocumentsFirstDb> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnName("Название");
            builder.Property(e => e.InvNumber).HasColumnName("Инв. номер");
            builder.HasIndex(e => e.InvNumber).IsUnique();
            builder.Property(e => e.Data).HasColumnName("Дата");
            builder.Property(e => e.VolumeId).HasColumnName("Id тома");
            builder.Property(e => e.InventoryId).HasColumnName("Id инвентаря");
            builder.Property(e => e.Page).HasColumnName("Страница");
            builder.Property(e => e.ForDestruction).HasColumnName("На уничтожение");
            builder.Property(e => e.DestructionMark).HasColumnName("Отметка об уничтожении");
            builder.Property(e => e.Note).HasColumnName("Примечание");
            builder.Property(e => e.CreateDate).HasColumnName("CreateDate");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            builder.Property(e => e.UpdateDate).HasColumnName("UpdateDate");
            builder.Property(e => e.UpdatedBy).HasColumnName("UpdatedBy");

            builder.HasMany(d => d.HardwaresDb).WithOne(h => h.DocumentFirstDb).HasForeignKey(h => h.DocLocationFirstId).OnDelete(DeleteBehavior.Restrict);


            builder.ToTable("Док. Первый");
        }

    }
}
