using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class ord_guanzhangriViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "类型")]
        public int? Leixing { get; set; }
        [Display(Name = "关帐日")]
        [DataType(DataType.Date)]
        public DateTime? Guanzhangri { get; set; }
        [Display(Name = "说明")]
        public string Memo { get; set; }
        [Display(Name = "机动1")]
        public string Col1 { get; set; }
        [Display(Name = "制单日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

