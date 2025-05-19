using Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Entities.People
{
    [Table("owner")]
    public class OwnerEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        [Required]
        [StringLength(10)]
        public string? Phone { get; set; }

        [Required]
        [Column(TypeName = "Text")]
        public string? Address { get; set; }

        public virtual PersonEntity? Person { get; set; }
    }
}
