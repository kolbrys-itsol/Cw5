using System.ComponentModel.DataAnnotations;

namespace Cw5.DTOs.Requests
{
    public class LoginRequest
    {
        [Required]
        public string Index { get; set; }
        [Required]
        public string Password { get; set; }
    }
}