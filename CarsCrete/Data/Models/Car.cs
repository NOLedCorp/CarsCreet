using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsCrete.Data.Models
{
    public class Car
    {
        #region Constructor
        public Car()
        {

        }
        #endregion
        #region Properties
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Photo { get; set; }

        [Required]
        public int Passengers { get; set; }

        [Required]
        public int Doors { get; set; }

        [Required]
        public string Transmission { get; set; }
        [Required]
        public string BodyType { get; set; }
        [Required]
        public string Contain { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool AC { get; set; }
        [Required]
        [DefaultValue(21)]
        public ushort MinAge { get; set; }
        [Required]
        [DefaultValue(true)]
        public bool Airbags { get; set; }
        [Required]
        [DefaultValue("Economy")]
        public string Groupe { get; set; }
        [Required]
        [DefaultValue(true)]
        public bool Radio { get; set; }
        [Required]
        [DefaultValue(true)]
        public bool ABS { get; set; }

        [Required]
        public string Fuel { get; set; }

        [Required]
        public int Consumption { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DefaultValue("Eng description of the car.")]
        public string Description_ENG { get; set; }

        [Required]
        public double Price { get; set; }
        [DefaultValue(0)]
        public double Mark { get; set; }
        #endregion
        #region Lazy-Load Properties
       
        //public virtual List<FeedBack> Reports { get; set; }
        //public virtual List<Book> Books { get; set; }
        //public virtual List<Sale> Sales { get; set; }
        #endregion
    }
}
