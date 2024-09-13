using Inspector.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inspector.Persistence.DbMaps
{
    public class HardwaresMap : IEntityTypeConfiguration<HardwaresDb>
    {
        public void Configure(EntityTypeBuilder<HardwaresDb> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.NameId).HasColumnName("Id Наим. ТС");
            builder.Property(e => e.SerialNumber).HasColumnName("Серийный номер");
            builder.HasIndex(e => e.SerialNumber).IsUnique();
            builder.Property(e => e.Model).HasColumnName("Модель");
            builder.Property(e => e.Appointment).HasColumnName("Назначение");
            builder.Property(e => e.CabinetId).HasColumnName("Id Кабинета");
            builder.Property(e => e.OvtId).HasColumnName("Id ОВТ");
            builder.Property(e => e.DocLocationFirstId).HasColumnName("Id Документа Первый");
            builder.Property(e => e.DocLocationSecondId).HasColumnName("Id Документа Второй");
            builder.Property(e => e.DocLocationThirdId).HasColumnName("Id Документа Третий.");
            builder.Property(e => e.UsageInfo).HasColumnName("Актуальность");
            builder.Property(e => e.Note).HasColumnName("Примечание");
            builder.Property(e => e.CreateDate).HasColumnName("CreateDate");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            builder.Property(e => e.UpdateDate).HasColumnName("UpdateDate");
            builder.Property(e => e.UpdatedBy).HasColumnName("UpdatedBy");
            builder.ToTable("Тех.средства");



        }
    }
}
