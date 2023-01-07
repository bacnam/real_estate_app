using Microsoft.EntityFrameworkCore;
using RealEstate.WebService.Models;

namespace RealEstate.WebService.Databases
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Sort).HasDefaultValue(0);
                entity.Property(e => e.Password).HasColumnName("password_hash");
                entity.Property(e => e.FullName).HasColumnName("name");
                entity.Property(e => e.PhoneNumber).HasColumnName("phone");
                entity.Property(e => e.Token).HasColumnName("auth_key");
                entity.Property(d => d.Created).ValueGeneratedOnAdd().HasColumnName("created_datetime");
                entity.Property(d => d.Updated).ValueGeneratedOnUpdate().HasColumnName("updated_datetime");
            });

            modelBuilder.Entity<CityModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Sort);
                entity.Property(d => d.Created).ValueGeneratedOnAdd();
                entity.Property(d => d.Updated).ValueGeneratedOnUpdate();
            });

            modelBuilder.Entity<DistrictModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Sort);
                entity.HasOne(e => e.City).WithMany().HasForeignKey("city");
                entity.Property(d => d.Created).ValueGeneratedOnAdd();
                entity.Property(d => d.Updated).ValueGeneratedOnUpdate();
            });

            modelBuilder.Entity<WardModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Sort);
                entity.HasOne(e => e.District).WithMany().HasForeignKey("DistrictId");
                entity.Property(d => d.Created).ValueGeneratedOnAdd();
                entity.Property(d => d.Updated).ValueGeneratedOnUpdate();
            });

            modelBuilder.Entity<StreetModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Sort);
                entity.HasOne(e => e.Ward).WithMany().HasForeignKey("ward");
                entity.Property(d => d.Created).ValueGeneratedOnAdd();
                entity.Property(d => d.Updated).ValueGeneratedOnUpdate();
            });

            modelBuilder.Entity<NewsModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).HasColumnName("tieude");
                entity.Property(e => e.NewsType).HasColumnName("loaitin");
                entity.Property(e => e.Sort);
                entity.Property(e => e.ReadTime);
                entity.Property(e => e.Description).HasColumnName("tomtat");
                entity.Property(e => e.Content).HasColumnName("noidung");
                entity.Property(e => e.Created).HasColumnName("created_datetime");
                entity.Property(e => e.Updated).HasColumnName("updated_datetime");
            });

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
                entity.Property(e => e.Room);
                entity.Property(e => e.Sort);
                entity.Property(e => e.Latitude);
                entity.Property(e => e.Longitude);
                entity.Property(e => e.ProjectId);
                entity.Property(e => e.IsSale).HasConversion<bool>().HasDefaultValue(false);
                entity.HasMany(e => e.Images).WithOne(e => e.RealEstate);
                entity.HasOne(e => e.Ward).WithMany().HasForeignKey("WardId");
                entity.HasOne(e => e.Direction).WithMany().HasForeignKey("huong");
                entity.HasOne(e => e.RealNewsType).WithMany().HasForeignKey("loai");
                entity.Property(e => e.StartAt).HasColumnName("StartAt");
                entity.Property(e => e.EndAt).HasColumnName("EndAt");
                entity.Property(e => e.Created).HasColumnName("created_datetime");
                entity.Property(e => e.Updated).HasColumnName("updated_datetime");

                entity.Property(e => e.CityId).HasColumnName("thanhpho");
                entity.Property(e => e.DistrictId).HasColumnName("quan");
            });

            modelBuilder.Entity<DirectionModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
            });

            modelBuilder.Entity<AccountRealEstateModel>(entity => {
                entity.HasKey(e => new { e.AccountId, e.RealEstateId });
                entity.HasOne(e => e.RealEstate).WithMany(e => e.AccountRealNews);
                entity.HasOne(e => e.Account).WithMany(e => e.AccountRealNews);
            });

            modelBuilder.Entity<ProjectModel>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Acreage);
                entity.Property(e => e.Address);
                entity.Property(e => e.ApartmentNumber);
                entity.Property(e => e.Description);
                entity.HasOne(e => e.District).WithMany();
                entity.Property(e => e.Floor);
                entity.Property(e => e.HandedOver);
                entity.Property(e => e.Name);
                entity.Property(e => e.Sort);
                entity.Property(e => e.Thumbnail);
                entity.Property(e => e.Created);
                entity.Property(e => e.Updated);
            });

            modelBuilder.Entity<AccountProjectModel>(entity => {
                entity.HasKey(e => new { e.AccountId, e.ProjectId });
                entity.HasOne(e => e.Project).WithMany(e => e.AccountProjects);
                entity.HasOne(e => e.Account).WithMany(e => e.AccountProjects);
            });

            modelBuilder.Entity<ImageModel>(entity => {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.RealEstate).WithMany(r => r.Images).HasForeignKey("batdongsan_id");
                entity.Property(e => e.FileName).HasColumnName("image");
            });

            modelBuilder.Entity<NotificationModel>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Sort);
                entity.Property(e => e.IsRead).HasConversion<bool>().HasDefaultValue(false);
                entity.Property(e => e.NewsId);
                entity.Property(e => e.RealId);
                entity.Property(e => e.Created);
                entity.Property(e => e.Updated);
            });
        }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<CityModel> Cities { get; set; }

        public DbSet<DistrictModel> Districts { get; set; }

        public DbSet<WardModel> Wards { get; set; }

        public DbSet<StreetModel> Streets { get; set; }

        public DbSet<DirectionModel> Directions { get; set; }

        public DbSet<NewsModel> News { get; set; }

        public DbSet<RealEstateModel> RealEstates { get; set; }

        public DbSet<RealEstateTypeModel> RealEstateTypes { get; set; }

        public DbSet<AccountRealEstateModel> AccountRealEstate { get; set; }

        public DbSet<ProjectModel> Projects { get; set; }

        public DbSet<AccountProjectModel> AccountProjects { get; set; }

        public DbSet<ImageModel> Images { get; set; }

        public DbSet<NotificationModel> Notifications { get; set; }
    }
}
