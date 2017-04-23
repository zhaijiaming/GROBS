using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class rmd_remindlistViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "信息")]
        public string Info { get; set; }
        [Display(Name = "链接")]
        public string Ref { get; set; }
    }
}