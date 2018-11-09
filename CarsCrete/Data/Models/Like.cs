﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CarsCrete.Data.Models
{
    public class Like
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long CommentId { get; set; }
        [Required]
        public long FeedBackId { get; set; }
        [Required]
        public bool IsLike { get; set; }

       

    }
}
