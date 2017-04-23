using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class auth_gongnengViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "模块")]
        public string Module { get; set; }
        [Display(Name = "控制")]
        public string Controller { get; set; }
        [Display(Name = "功能")]
        public string Function { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "等级")]
        public int? Grade { get; set; }
        [Display(Name = "禁用")]
        public bool? Forbid { get; set; }
        [Display(Name = "日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

