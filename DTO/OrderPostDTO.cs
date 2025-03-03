using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record OrderPostDTO(int? UserId, DateOnly? OrderDate, double? OrderSum, ICollection<OrderItemDTO> OrderItems,string? UserFirstName,string? UserLastName);
    //int? OrderId,

}
