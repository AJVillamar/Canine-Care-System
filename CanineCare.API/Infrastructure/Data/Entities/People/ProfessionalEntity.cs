using Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Entities.People
{
    [Table("professional")]
    public class ProfessionalEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public int YearsOfExperience { get; set; }

        public virtual PersonEntity? Person { get; set; }
    }
}
