using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MySql.EntityFrameworkCore.DataAnnotations;

namespace RealEstate.WebService.Models
{
    [Table("users")]
    [MySqlCharset("utf8")]
    public class UserModel : BaseModel
    {

        [Column(TypeName = "varchar(255)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Password { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string FullName { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Token { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string FacebookId { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string GoogleId { get; set; }

        public ICollection<AccountRealEstateModel> AccountRealNews { get; set; }

        public ICollection<AccountProjectModel> AccountProjects { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.DateTime)]
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}
