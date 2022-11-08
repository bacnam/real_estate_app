using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.EntityFrameworkCore.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.WebService.Models
{
    [Table("hinhanh")]
    [MySqlCharset("utf8")]
    public class ImageModel : BaseModel
    {
        [Column("image", TypeName = "varchar(255)")]
        public string FileName { get; set; }

        public RealEstateModel RealEstate { get; set; }
    }
}
