using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarsCrete.Data.Models
{
    public class Sale
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        public long CarId { get; set; }
        [Required]
        public ushort Type { get; set; }
        [Required]
        public DateTime DateStart { get; set; }
        [Required]
        public DateTime DateFinish { get; set; }
        [Required]
        public double NewPrice { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        [DefaultValue(1)]
        public int DaysNumber { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }

    }
}
