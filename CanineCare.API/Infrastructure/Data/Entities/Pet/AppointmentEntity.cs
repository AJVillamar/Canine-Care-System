using Infrastructure.Common;
using Infrastructure.Data.Entities.People;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Entities.Pet
{
    [Table("appoinment")]
    public class AppointmentEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        [Required]
        public Guid PetId { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly Time { get; set; }

        [Required]
        public Guid ProfessionalId { get; set; }

        [Column( TypeName = "Text" )]
        public string? Reason { get; set; }

        [Required]
        public string? Status { get; set; }

        public virtual ServiceEntity? Service { get; set; }

        public virtual PetEntity? Pet { get; set; }

        public virtual ProfessionalEntity? Professional { get; set; }
    }
}
