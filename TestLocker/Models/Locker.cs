using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestLocker.Models
{
    public class Locker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }

        public string OwnerId { get; set; }
        [JsonIgnore]
        public virtual AppUser Owner { get; set; }

        [Required]
        public int AllowedTime { get; set; }

        [MaxLength(200)]
        public string Link { get; set; }

        public DateTime? AccessTime { get; set; }
        public DateTime? SubmitTime { get; set; }
    }
}
