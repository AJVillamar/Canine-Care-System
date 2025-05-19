using Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Entities.People
{
    [Table("admin")]
    public class AdminEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public virtual PersonEntity? Person { get; set; }
    }
}
