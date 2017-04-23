using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class rmd_myreminderViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "用户")]
        public int? YonghuID { get; set; }
        [Display(Name = "提醒对象")]
        public int? TixingID { get; set; }
        [Display(Name = "提醒区间")]
        public int? TixingZQ { get; set; }
        [Display(Name = "主页显示")]
        public bool YemianXS { get; set; }
        [Display(Name = "制单日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

