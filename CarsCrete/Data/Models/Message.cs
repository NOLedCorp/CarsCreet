using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarsCrete.Data.Models
{
    public class Message
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long UserReciverId { get; set; }



    }
}
