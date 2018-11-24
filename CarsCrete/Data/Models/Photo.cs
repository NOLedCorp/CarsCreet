using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarsCrete.Data.Models
{
    public class Photo
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public long PhotoOwnerId { get; set; }
        [Required]
        public string Path { get; set; }
    }
}
