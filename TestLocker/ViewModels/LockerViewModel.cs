using System.ComponentModel.DataAnnotations;

namespace TestLocker.ViewModels
{
    public class LockerViewModel
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }

        public int AllowedTime { get; set; }

        [MaxLength(200)]
        public string Link { get; set; }

    }
}
