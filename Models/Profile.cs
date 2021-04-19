using System.ComponentModel.DataAnnotations;

namespace contractor.Models
{
    public class Profile
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Picture { get; set; }

    }
}