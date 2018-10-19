﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsCrete.Data.Models
{
    public class BookDTO
    {
        #region Constructor
        public BookDTO()
        {

        }
        #endregion
        #region Properties
        public long Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
        public long UserId { get; set; }
        public long CarId { get; set; }
        public double Price { get; set; }
        public string Place { get; set; }
        #endregion
    }
}
