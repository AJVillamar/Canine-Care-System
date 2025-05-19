using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Common
{
    public abstract class BaseEntity
    {
        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
