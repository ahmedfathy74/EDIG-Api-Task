using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos
{
    public class StockDto
    {
        public int Id { get; set; }
        public string StockName { get; set; } = null!;
    }
}
