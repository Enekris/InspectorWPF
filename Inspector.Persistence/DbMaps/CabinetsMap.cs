using Inspector.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inspector.Persistence.DbMaps
{
    public class CabinetsMap : IEntityTypeConfiguration<CabinetsDb>
    {
        public void Configure(EntityTypeBuilder<CabinetsDb> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Number).HasColumnName("Номер");
            builder.Property(e => e.Address).HasColumnName("Адрес здания");
            builder.Property(e => e.Status).HasColumnName("Тип помещения");
            builder.Property(e => e.ResponsibleExp).HasColumnName("Отв. за экспл.");
            builder.Property(e => e.ResponsibleTZI).HasColumnName("Отв. за ТЗИ");
            builder.Property(e => e.SertificateId).HasColumnName("Id аттестата");
            builder.Property(e => e.RaspExpId).HasColumnName("Id Расп. о вводе в экспл.");
            builder.Property(e => e.ActReportId).HasColumnName("Id акта обслед.");
            builder.Property(e => e.Persons).HasColumnName("Список допущенных в кабинет.");
            builder.Property(e => e.Note).HasColumnName("Примечание");
            builder.Property(e => e.CreateDate).HasColumnName("CreateDate");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            builder.Property(e => e.UpdateDate).HasColumnName("UpdateDate");
            builder.Property(e => e.UpdatedBy).HasColumnName("UpdatedBy");


            builder.HasMany(c => c.HardwaresDb).WithOne(h => h.CabinetDb).HasForeignKey(h => h.CabinetId).OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Кабинеты");

        }
    }
}
