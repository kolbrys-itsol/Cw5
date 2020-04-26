using System.ComponentModel.DataAnnotations;

namespace Cw5.DTOs.Requests
{
    public class ChangePasswordRequest
    {
        [Required]
        public string Index { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}