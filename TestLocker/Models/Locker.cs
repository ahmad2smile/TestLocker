using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace TestLocker.Models
{
    [DataContract]
    public class Locker
    {
        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string OwnerId { get; set; }

        public virtual AppUser Owner { get; set; }

        [Required]
        [DataMember]
        public int AllowedTime { get; set; }

        [MaxLength(200)]
        [DataMember]
        public string Link { get; set; }

        [DataMember]
        public DateTime? AccessTime { get; set; }
        [DataMember]
        public DateTime? SubmitTime { get; set; }
    }
}
