using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsCrete.Data.Models
{
    public class MessageDTO
    {
        public long Id { get; set; }
       
        public DateTime CreateDate { get; set; }
      
        public string Text { get; set; }
        
        public long UserId { get; set; }
        
        public long UserReciverId { get; set; }

        public virtual UserDTO User { get; set; }
    }
}
