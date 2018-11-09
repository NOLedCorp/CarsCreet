using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsCrete.Data.Models
{
    public class FeedBackDTO
    {
        #region Constructor
        public FeedBackDTO()
        {

        }
        #endregion
        #region Properties

        public long Id { get; set; }
        public long UserId { get; set; }
        public long CarId { get; set; }
        public double Look { get; set; }
        public double Comfort { get; set; }
        public double Drive { get; set; }
        public double Mark { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ReportUser User { get; set; }
        public virtual Car Car { get; set; }
        public virtual List<ReportCommentDTO> Comments { get; set; }
        public virtual List<LikeDTO> Likes { get; set; }
        #endregion
    }
}
