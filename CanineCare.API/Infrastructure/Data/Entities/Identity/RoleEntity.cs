using Infrastructure.Common;
using Infrastructure.Data.Entities.People;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Entities.Identity
{
    [Table("role")]
    public class RoleEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public virtual ICollection<PersonEntity> Persons { get; set; } = new List<PersonEntity>();
    }
}
