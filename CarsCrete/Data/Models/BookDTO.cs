using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsCrete.Data.Models
{
    public class BookDTO
    {
        #region Constructor
        public BookDTO()
        {

        }
        #endregion
        #region Properties
        public long Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime ExtraDateStart { get; set; }
        public DateTime DateFinish { get; set; }
        public long UserId { get; set; }
        public long CarId { get; set; }
        public long SalesId { get; set; }
        public double Price { get; set; }
        public string Place { get; set; }
        public double OldPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public double Sum { get; set; }
        #endregion
        #region Lazy-loading
        public virtual UserDTO User { get; set; }
        public virtual CarDTO Car { get; set; }
        public virtual SaleDTO Sale { get; set; }
        
        #endregion
    }
}
