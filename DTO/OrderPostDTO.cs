using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record OrderPostDTO(DateOnly? OrderDate, int? OrderSum, ICollection<OrderItemDTO>? OrderItems,int? UserId);
    //int? OrderId,

}
