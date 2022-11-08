using Microsoft.EntityFrameworkCore;
using RealEstate.WebService.Models;

namespace RealEstate.WebService.Databases
{
    public class ImageContext : DbContext
    {
        string _connectionString = string.Empty;
        public void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
            optionsBuilder.EnableSensitiveDataLogging(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RealEstateModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).HasColumnName("tieude");
                entity.Property(e => e.Description).HasColumnName("mota");
                entity.Property(e => e.Contact).HasColumnName("contact_name");
                entity.Property(e => e.ContactPhone).HasColumnName("contact_phone");
                entity.Property(e => e.Acreage).HasColumnName("dientich_matbang");
                entity.Property(e => e.Address).HasColumnName("diachi");
                entity.Property(e => e.Price).HasColumnName("gia");
                entity.Property(e => e.PriceType).HasColumnName("kieugia");
                entity.Property(e => e.Sort);
                entity.Property(e => e.Latitude);
                entity.Property(e => e.Longitude);
                entity.Property(e => e.ProjectId);
                entity.Property(e => e.IsSale).HasConversion<ushort>().HasDefaultValue(false);
                entity.HasMany(e => e.Images).WithOne(e => e.RealEstate);
                entity.HasOne(e => e.Ward).WithMany().HasForeignKey("WardId");
                entity.HasOne(e => e.Direction).WithMany().HasForeignKey("huong");
                entity.HasOne(e => e.RealNewsType).WithMany().HasForeignKey("loai");
                entity.Property(e => e.StartAt).HasColumnName("StartAt");
                entity.Property(e => e.EndAt).HasColumnName("EndAt");
                entity.Property(e => e.Created).HasColumnName("created_datetime");
                entity.Property(e => e.Updated).HasColumnName("updated_datetime");
            });

            modelBuilder.Entity<ImageModel>(entity => {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.RealEstate).WithMany(r => r.Images).HasForeignKey("batdongsan_id");
                entity.Property(e => e.FileName).HasColumnName("image");
            });
        }

        public DbSet<RealEstateModel> RealEstates { get; set; }

        public DbSet<ImageModel> Images { get; set; }
    }
}