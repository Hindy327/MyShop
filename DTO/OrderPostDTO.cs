using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record OrderPostDTO(int? OrderId, DateOnly? OrderDate, int? OrderSum, ICollection<OrderItemDTO>? OrderItems,int? UserId);

}
