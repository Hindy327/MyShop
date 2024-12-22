using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record OrderDTO(int OrderId, DateOnly? OrderDate, int? OrderSum, string UserFirstName, string UserLastName);
}
