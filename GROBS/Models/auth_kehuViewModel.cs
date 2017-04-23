using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class auth_kehuViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "客户序号")]
        public int? KehuID { get; set; }
        [Display(Name = "帐号")]
        public string Zhanghao { get; set; }
        [Display(Name = "密码")]
        public string Mima { get; set; }
        [Display(Name = "昵称")]
        public string Nicheng { get; set; }
        [Display(Name = "是否停用")]
        public bool TingyongSF { get; set; }
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

