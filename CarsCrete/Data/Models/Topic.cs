using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarsCrete.Data.Models
{
    public class Topic
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long UserReciverId { get; set; }
        [Required]
        public DateTime ModifyDate { get; set; }


        public virtual List<Message> Messages { get; set; }
    }
}
