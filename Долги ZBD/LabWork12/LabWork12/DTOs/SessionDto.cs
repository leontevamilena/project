using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12.DTOs
{
    public class SessionDto
    {

        public SessionDto(decimal minPrice, decimal maxPrice, decimal averagePrice)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            AveragePrice = averagePrice;
        }

        public decimal MinPrice { get; }
        public decimal MaxPrice { get; }
        public decimal AveragePrice { get; }
    }
}
