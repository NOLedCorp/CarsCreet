using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsCrete.Data.Models
{
    public class SaleDTO
    {
        public long Id { get; set; }
        public long CarId { get; set; }
        public ushort Type { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
        public double NewPrice { get; set; }
        public double Discount { get; set; }
        public int DaysNumber { get; set; }
        public virtual Car Car { get; set; }
    }
}
