using MySql.EntityFrameworkCore.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.WebService.Models
{
    [Table("account_project")]
    [MySqlCharset("utf8")]
    public class AccountProjectModel : BaseModel
    {
        public long AccountId { get; set; }

        public UserModel Account { get; set; }

        public long ProjectId { get; set; }

        public ProjectModel Project { get; set; }
    }
}
