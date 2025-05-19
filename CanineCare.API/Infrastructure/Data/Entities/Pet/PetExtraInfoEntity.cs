using Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Entities.Pet
{
    [Table("pet-extrainfo")]
    public class PetExtraInfoEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]  
        public Guid PetId { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public string? Allergies { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public string? PreExistingConditions { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public string? SpecialCareInstructions { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public string? FeedingNotes { get; set; }

        public virtual PetEntity? Pet { get; set; }
    }
}
