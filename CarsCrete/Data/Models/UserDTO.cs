using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsCrete.Data.Models
{
    public class UserDTO        
    {
        #region Constructor
        public UserDTO()
        {

        }
        #endregion

        #region Properties
        public long Id { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Lang { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<FeedBackDTO> Reports { get; set; }
        public List<BookDTO> Books { get; set; }
        public virtual List<ReportComment> Comments { get; set; }
        public virtual List<TopicDTO> Topics { get; internal set; }
        #endregion

    }
}
