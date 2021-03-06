﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CarsCrete.Data.Models
{
    public class User
    {
        #region Constructor
        public User()
        {

        }
        #endregion

        #region Properties
        [Key]
        [Required]
        public long Id { get; set; }
        [DefaultValue("../../assets/images.default_user_photo.jpg")]
        public string Photo { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Lang { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool IsAdmin { get; set; }
        #endregion

        #region Lazy-Load Properties
        /// <summary>
        /// A list containing all the reports related to this user.
        /// It will be populaed on first use thanks to the EF Lazy-Loading feature.
        /// </summary>
        public virtual List<FeedBack> Reports { get; set; }
        public virtual List<Book> Books { get; set; }
        public virtual List<ReportComment> Comments { get; set; }
        public virtual List<Topic> Topics { get; internal set; }
        #endregion

    }
}
