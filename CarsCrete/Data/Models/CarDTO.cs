using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsCrete.Data.Models
{
    public class CarDTO             
    {
        #region Constructor
        public CarDTO()
        {

        }
        #endregion
        #region Properties
        public long Id { get; set; }
        public string Model { get; set; }
        public string Photo { get; set; }
        public int Passengers { get; set; }
        public int Doors { get; set; }
        public string Transmission { get; set; }
        public string Fuel { get; set; }
        public string Consumption { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        #endregion
    }
}
