using Infrastructure.Common;
using Infrastructure.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Entities.People
{
    [Table("person")]
    public class PersonEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(10)]
        public string? Identification { get; set; }

        [Required]
        [StringLength(255)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string? LastName { get; set; }

        [StringLength(255)]
        public string? Email { get; set; }

        public virtual UserEntity? User { get; set; }

        public virtual ICollection<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    }
}
