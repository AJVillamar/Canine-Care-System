using Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Entities.Pet
{
    [Table("service-detail")]
    public class ServiceDetailEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        [Required]
        [Column(TypeName = "Text")]
        public string? ActionName { get; set; }

        [Required]
        [Column(TypeName = "Text")]
        public string? ActionDescription { get; set; }

        public virtual ServiceEntity? Service { get; set; }
    }
}
