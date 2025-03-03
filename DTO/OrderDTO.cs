using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
    
{
   
    public record OrderDTO(int OrderId, DateOnly OrderDate, double? OrderSum, string UserFirstName);

}
