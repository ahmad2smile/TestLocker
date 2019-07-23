using System.ComponentModel.DataAnnotations;

namespace TestLocker.ViewModels
{
    public class LockerViewModel
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public int AllowedTime { get; set; }

        [Required]
        [MaxLength(200)]
        public string Link { get; set; }

    }
}
