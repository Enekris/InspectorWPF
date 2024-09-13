using Inspector.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inspector.Persistence.DbMaps
{
    public class OVTsMap : IEntityTypeConfiguration<OVTsDb>
    {
        public void Configure(EntityTypeBuilder<OVTsDb> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnName("Наименование");
            builder.Property(e => e.SertificateId).HasColumnName("Id аттестата");
            builder.HasIndex(e => e.SertificateId).IsUnique();
            builder.Property(e => e.ResponsibleExp).HasColumnName("Отв. за эксплуатацию");
            builder.Property(e => e.ResponsibleTZI).HasColumnName("Отв. за ТЗИ");
            builder.Property(e => e.AdminSec).HasColumnName("Админ.Безоп.Инф.");
            builder.Property(e => e.AdminSys).HasColumnName("Сис.Админ.");
            builder.Property(e => e.RaspExpId).HasColumnName("Id Расп. о вводе в экспл.");
            builder.Property(e => e.Note).HasColumnName("Примечание");
            builder.Property(e => e.CreateDate).HasColumnName("CreateDate");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            builder.Property(e => e.UpdateDate).HasColumnName("UpdateDate");
            builder.Property(e => e.UpdatedBy).HasColumnName("UpdatedBy");

            builder.HasMany(o => o.HardwaresDb).WithOne(h => h.OVTDb).HasForeignKey(h => h.OvtId).OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("ОВТ");

        }
    }
}
