using Infrastructure.Common;
using Infrastructure.Data.Entities.People;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Entities.Pet
{
    [Table("pet")]
    public class PetEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string? Name { get; set; }

        [Required]
        public Guid BreedId { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(255)]
        public string? Sex { get; set; }

        [Required]
        [StringLength(255)]
        public string? Color { get; set; }

        [Required]
        public double Weight { get; set; }

        public Guid OwnerId { get; set; }

        public virtual BreedEntity? Breed { get; set; }

        public virtual OwnerEntity? Owner { get; set; }

        public virtual PetExtraInfoEntity? PetExtraInfo { get; set; }
    }
}
