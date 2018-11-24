using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsCrete.Data.Models
{
    public class PhotoDTO
    {
        public long Id { get; set; }
        public long PhotoOwnerId { get; set; }
        public string Path { get; set; }
    }
}
