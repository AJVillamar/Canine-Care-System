using Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Entities.Pet
{
    [Table("services")]
    public class ServiceEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(500)]
        public string? Name { get; set; }

        [Required]
        [Column(TypeName = "Text")]
        public string? Description { get; set; }

        [Required]
        [StringLength(500)]
        public string? Type { get; set; }

        public virtual ICollection<ServiceDetailEntity> ServiceDetails { get; set; } = new List<ServiceDetailEntity>();
    }
}
