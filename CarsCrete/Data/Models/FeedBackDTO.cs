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
        public short Mark { get; set; }
        public string Text { get; set; }
        #endregion
    }
}
