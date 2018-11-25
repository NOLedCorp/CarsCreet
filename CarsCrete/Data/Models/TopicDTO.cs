using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsCrete.Data.Models
{
    public class TopicDTO
    {

        public long Id { get; set; }
        public long UserId { get; set; }
        public long UserReciverId { get; set; }
        public bool Seen { get; set; }
        public DateTime ModifyDate { get; set; }
        public virtual ReportUser User { get; set; }
        public virtual List<MessageDTO> Messages { get; set; }
    }
}
