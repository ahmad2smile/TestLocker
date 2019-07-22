using System;
using System.ComponentModel.DataAnnotations;
using TestLocker.ViewModels;

namespace TestLocker.Models
{
    public class Locker : LockerViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime AccessTime { get; set; }
        public DateTime SubmitTime { get; set; }
    }
}
