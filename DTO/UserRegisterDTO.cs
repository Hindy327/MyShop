using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record UserRegisterDTO(int? UserId, string? Email, string? Password, string? FirstName, string? LastName);
}
