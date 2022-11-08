using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MySql.EntityFrameworkCore.DataAnnotations;

namespace RealEstate.WebService.Models
{
    [Table("rooms")]
    [MySqlCharset("utf8")]
    public class RoomModel : BaseModel
    {
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.DateTime)]
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}
