using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.EntityFrameworkCore.DataAnnotations;

namespace RealEstate.WebService.Models
{
    [Table("wards")]
    [MySqlCharset("utf8")]
    public class WardModel : BaseModel
    {
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        public DistrictModel District { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.DateTime)]
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}
