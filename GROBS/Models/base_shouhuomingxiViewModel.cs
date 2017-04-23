using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class base_shouhuomingxiViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "收货单位序号")]
        public int ShouhuofangID { get; set; }
        [Display(Name = "名称")]
        public string Mingcheng { get; set; }
        [Display(Name = "地址")]
        public string Dizhi { get; set; }
        [Display(Name = "联系人")]
        public string Lianxiren { get; set; }
        [Display(Name = "联系电话")]
        public string Lianxidianhua { get; set; }
        [Display(Name = "货主的销售序号")]
        public int? XiaoshouID { get; set; }
        [Display(Name = "货主的销售姓名")]
        public string Xiaoshouren { get; set; }
        [Display(Name = "机动1")]
        public string Col1 { get; set; }
        [Display(Name = "机动2")]
        public string Col2 { get; set; }
        [Display(Name = "机动3")]
        public string Col3 { get; set; }
        [Display(Name = "录入日期")]
        [DataType(DataType.Date)]
        public DateTime MakeDate { get; set; }
        [Display(Name = "输入人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

