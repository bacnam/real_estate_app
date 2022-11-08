using MySql.EntityFrameworkCore.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.WebService.Models
{
    [Table("account_real_news")]
    [MySqlCharset("utf8")]
    public class AccountRealEstateModel : BaseModel
    {
        public long AccountId { get; set; }

        public UserModel Account { get; set; }

        public long RealEstateId { get; set; }

        public RealEstateModel RealEstate { get; set; }
    }
}
