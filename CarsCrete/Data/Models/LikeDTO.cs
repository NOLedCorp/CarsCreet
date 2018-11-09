using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CarsCrete.Data.Models
{
    public class LikeDTO
    {
     
        public long Id { get; set; }
     
        public long UserId { get; set; }
      
        public long CommentId { get; set; }
    
        public long FeedBackId { get; set; }
      
        public bool IsLike { get; set; }
        
        public virtual ReportCommentDTO Comment { get; set; }
       
        public virtual FeedBackDTO Report { get; set; }

    }
}
