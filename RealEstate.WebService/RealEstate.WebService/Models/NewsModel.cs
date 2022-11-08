using System.ComponentModel.DataAnnotations.Schema;
using MySql.EntityFrameworkCore.DataAnnotations;

namespace RealEstate.WebService.Models
{
    [Table("tin")]
    [MySqlCharset("utf8")]
    public class NewsModel : BaseModel
    {
        public string Title { get; set; }

        public string NewsType { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string Thumbnail { get; set; }

        public int ReadTime { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
