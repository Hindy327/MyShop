using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    
        public record ProductDTO(string? Name, double? price, string? Description, string? Image,string? categoryName);
    
}
