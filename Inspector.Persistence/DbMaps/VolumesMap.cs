using Inspector.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inspector.Persistence.DbMaps
{
    public class VolumesMap : IEntityTypeConfiguration<VolumesDb>
    {
        public void Configure(EntityTypeBuilder<VolumesDb> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.InvNumber).HasColumnName("Инвент. Номер");
            builder.Property(e => e.VolumeNumber).HasColumnName("Номер Тома");
            builder.Property(e => e.CaseNumber).HasColumnName("Номер дела");
            builder.Property(e => e.CaseYear).HasColumnName("Год Тома/Дела");
            builder.Property(e => e.ForDestruction).HasColumnName("На уничтожение");
            builder.Property(e => e.DestructionMark).HasColumnName("Отметка об уничтожении");
            builder.Property(e => e.Note).HasColumnName("Примечание");
            builder.Property(e => e.CreateDate).HasColumnName("CreateDate");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            builder.Property(e => e.UpdateDate).HasColumnName("UpdateDate");
            builder.Property(e => e.UpdatedBy).HasColumnName("UpdatedBy");

            builder.HasMany(v => v.DocumentRaspOVVDb).WithOne(d => d.VolumeDb).HasForeignKey(d => d.VolumeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(v => v.DocumentActReportDb).WithOne(d => d.VolumeDb).HasForeignKey(d => d.VolumeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(v => v.DocumentThirdDb).WithOne(d => d.VolumeDb).HasForeignKey(d => d.VolumeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(v => v.DocumentFirstDb).WithOne(d => d.VolumeDb).HasForeignKey(d => d.VolumeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(v => v.DocumentSecondDb).WithOne(d => d.VolumeDb).HasForeignKey(d => d.VolumeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(v => v.SertificatesDb).WithOne(s => s.VolumeDb).HasForeignKey(s => s.VolumeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(i => i.DocumentsOthersDb).WithOne(s => s.VolumeDb).HasForeignKey(s => s.VolumeId).OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Тома");
        }
    }
}
