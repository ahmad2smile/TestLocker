using System;
using System.ComponentModel.DataAnnotations;

namespace TestLocker.Models
{
    public class Locker
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime AccessTime { get; set; }
        public int AllowedTime { get; set; }
        public DateTime SubmitTime { get; set; }
    }
}
