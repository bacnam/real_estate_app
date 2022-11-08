using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.EntityFrameworkCore.DataAnnotations;

namespace RealEstate.WebService.Models
{
    [Table("projects")]
    [MySqlCharset("utf8")]
    public class ProjectModel : BaseModel
    {
        public ProjectModel()
        {
        }

        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Investor { get; set; }

        public DistrictModel District { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Address { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Thumbnail { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Description { get; set; }

        public int Floor { get; set; }

        public double Acreage { get; set; }

        public double AcreageFloor { get; set; }

        public int ApartmentNumber { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string HandedOver { get; set; }

        public ICollection<AccountProjectModel> AccountProjects { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.DateTime)]
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}
