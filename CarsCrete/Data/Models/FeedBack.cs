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
        public short Mark { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        #endregion

        #region Lazy-Load Properties
        /// <summary>
        /// A list containing all the results related to this quiz.
        /// It will be populaed on first use thanks to the EF Lazy-Loading feature.
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        /// <summary>
        /// A list containing all the results related to this quiz.
        /// It will be populaed on first use thanks to the EF Lazy-Loading feature.
        /// </summary>
        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }
        #endregion

    }
}
