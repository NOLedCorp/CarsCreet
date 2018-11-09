using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsCrete.Data.Models
{
    public class FeedBack
    {
        #region Constructor
        public FeedBack()
        {

        }
        #endregion
        #region Properties

        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long CarId { get; set; }
        [Required]
        public double Look { get; set; }
        [Required]
        public double Comfort { get; set; }
        [Required]
        public double Drive { get; set; }
        [Required]
        public double Mark { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        #endregion

        #region Lazy-loading
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }

        public virtual List<ReportComment> Comments { get; set; }
        public virtual List<Like> Likes { get; set; }
        #endregion



    }
}
