using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string PersonName { get; set; } = null!;
        public int StockID { get; set; }
    }
}
