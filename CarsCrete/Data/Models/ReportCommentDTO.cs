using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarsCrete.Data.Models
{
    public class ReportCommentDTO
    {
        #region Constructor
        public ReportCommentDTO()
        {

        }
        #endregion
        #region Properties
        public long Id { get; set; }
        public long UserId { get; set; }
        public long FeedBackId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        #endregion

        #region Lazy-loading
        public virtual ReportUser User { get; set; }
        public virtual List<LikeDTO> Likes { get; set; }
        #endregion
    }
}
