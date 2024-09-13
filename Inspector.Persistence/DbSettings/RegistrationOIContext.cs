using Inspector.Domains.Entities;
using Inspector.Persistence.DbMaps;
using Microsoft.EntityFrameworkCore;

namespace Inspector.Persistence.DbSettings
{
    public class RegistrationOIContext : DbContext
    {

        public virtual DbSet<CabinetsDb> Cabinets { get; set; } = null!;

        public virtual DbSet<DocumentsRaspOVVDb> DocumentsRaspOVV { get; set; } = null!;
        public virtual DbSet<DocumentsActReportDb> DocumentsActReport { get; set; } = null!;
        public virtual DbSet<DocumentsThirdDb> DocumentsThird { get; set; } = null!;
        public virtual DbSet<DocumentsFirstDb> DocumentsFirst { get; set; } = null!;
        public virtual DbSet<DocumentsSecondDb> DocumentsSecond { get; set; } = null!;
        public virtual DbSet<DocumentsOthersDb> DocumentsOthers { get; set; } = null!;
        // public virtual DbSet<DocumentsDb> Documents { get; set; } = null!;

        public virtual DbSet<HardwareFilterNameDb> HardwareFilter { get; set; } = null!;
        public virtual DbSet<HardwaresDb> Hardwares { get; set; } = null!;
        public virtual DbSet<InvertoriesDb> Invertories { get; set; } = null!;
        public virtual DbSet<OVTsDb> OVTs { get; set; } = null!;
        public virtual DbSet<SertificatesDb> Sertificates { get; set; } = null!;
        public virtual DbSet<VolumesDb> VolumeDb { get; set; } = null!;


        public RegistrationOIContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost;Database=Registration_02.09;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");
        //}
        public RegistrationOIContext(DbContextOptions<RegistrationOIContext> options)
    : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CabinetsMap());
            modelBuilder.ApplyConfiguration(new DocumentsRaspOVVMap());
            modelBuilder.ApplyConfiguration(new DocumentsActReportMap());
            modelBuilder.ApplyConfiguration(new DocumentsThirdMap());
            modelBuilder.ApplyConfiguration(new DocumentsOthersMap());
            modelBuilder.ApplyConfiguration(new DocumentsFirstMap());
            modelBuilder.ApplyConfiguration(new DocumentsSecondMap());
            modelBuilder.ApplyConfiguration(new HardwareFilterNameMap());
            modelBuilder.ApplyConfiguration(new HardwaresMap());
            modelBuilder.ApplyConfiguration(new InvertoriesMap());
            modelBuilder.ApplyConfiguration(new OVTsMap());
            modelBuilder.ApplyConfiguration(new SertificatesMap());
            modelBuilder.ApplyConfiguration(new VolumesMap());

        }

    }
}
