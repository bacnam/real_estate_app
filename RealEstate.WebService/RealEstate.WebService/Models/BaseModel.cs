using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.WebService.Models
{
    public class BaseModel
    {
        [Key]
        public long Id { get; set; }

        [Column(TypeName = "int(11)")]
        public int Sort { get; set; }
    }
}
