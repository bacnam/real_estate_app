using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.EntityFrameworkCore.DataAnnotations;

namespace RealEstate.WebService.Models
{
    [Table("real_estates")]
    [MySqlCharset("utf8")]
    public class RealEstateModel : BaseModel
    {
        [Column(TypeName = "varchar(255)")]
        public string Title { get; set; }

        public double Acreage { get; set; }

        public string Address { get; set; }

        public double Price { get; set; }

        public string PriceType { get; set; }

        public int Room { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Description { get; set; }

        public long CreatorId { get; set; }

        public long ProjectId { get; set; }

        public string Contact { get; set; }

        public string ContactPhone { get; set; }

        [Column(TypeName = "tinyint(1)")]
        public bool IsSale { get; set; }

        public string Attachment { get; set; }

        public WardModel Ward { get; set; }

        public int CityId { get; set; }

        public int DistrictId { get; set; }

        public RealEstateTypeModel RealNewsType { get; set; }

        public DirectionModel Direction { get; set; }

        public ICollection<ImageModel> Images { get; set; }

        public ICollection<AccountRealEstateModel> AccountRealNews { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime EndAt { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.DateTime)]
        public DateTime Updated { get; set; } = DateTime.UtcNow;

        public string GetFullAddress()
        {
            string fullAddress = "";
            fullAddress += Address;
            if (Ward != null)
            {
                fullAddress += " " + Ward.Name;
                if (Ward.District != null)
                {
                    fullAddress += " " + Ward.District.Name;
                    if (Ward.District.City != null)
                    {
                        fullAddress += " " + Ward.District.City.Name;
                    }
                }
            }
            return fullAddress;
        }
    }
}