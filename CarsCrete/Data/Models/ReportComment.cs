using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CarsCrete.Data.Models
{
    public class ReportComment
    {
        #region Constructor
        public ReportComment()
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
        public long FeedBackId { get; set; }
        [Required]
        [DefaultValue(0)]
        public int Likes { get; set; }
        [Required]
        [DefaultValue(0)]
        public int Dislikes { get; set; }

        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        #endregion

        #region Lazy-loading
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        #endregion
    }
}
