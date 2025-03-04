using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record UserRegisterDTO(int? UserId,[Required,EmailAddress(ErrorMessage ="not valid email adress")] string? Email, [Required]string? Password, [Required] string? FirstName, [Required] string? LastName);
}
