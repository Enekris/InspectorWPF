using Inspector.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inspector.Persistence.DbMaps
{

    public class SertificatesMap : IEntityTypeConfiguration<SertificatesDb>
    {
        public void Configure(EntityTypeBuilder<SertificatesDb> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnName("Наименование");
            builder.Property(e => e.Number).HasColumnName("Номер");
            builder.HasIndex(e => e.Number).IsUnique();
            builder.Property(e => e.DataFirst).HasColumnName("Дата Выдачи");
            builder.Property(e => e.DataEnd).HasColumnName("Срок действия");
            builder.Property(e => e.Organisation).HasColumnName("Кем выдан");
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

            builder.HasOne(s => s.OVTDb).WithOne(o => o.SertificateDb).HasForeignKey<OVTsDb>(o => o.SertificateId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(s => s.CabinetsDb).WithOne(c => c.SertificateDb).HasForeignKey(c => c.SertificateId).OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Аттестаты");


        }

    }
}
