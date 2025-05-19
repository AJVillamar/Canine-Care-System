using Infrastructure.Common;
using Infrastructure.Data.Entities.People;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Entities.Identity
{
    [Table("user")]
    public class UserEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        [StringLength(1000)]
        public string? Password { get; set; }

        public virtual PersonEntity? Person { get; set; }
    }
}
