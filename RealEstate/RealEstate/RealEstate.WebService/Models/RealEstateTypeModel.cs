using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.EntityFrameworkCore.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.WebService.Models
{
    [Table("categories")]
    [MySqlCharset("utf8")]
    public class RealEstateTypeModel : BaseModel
    {
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }
    }
}
