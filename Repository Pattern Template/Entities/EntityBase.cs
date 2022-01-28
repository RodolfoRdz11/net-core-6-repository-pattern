using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public abstract class EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public bool Deleted { get; set; } = false;
        public Guid? CreatedById { get; set; }
        public DateTime CreatedAt { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd H:mm"));
        public Guid? UpdatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? DeletedById { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}