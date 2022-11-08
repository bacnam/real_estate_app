using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.EntityFrameworkCore.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.WebService.Models
{
    [Table("huong")]
    [MySqlCharset("utf8")]
    public class DirectionModel : BaseModel
    {
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }
    }
}
