using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class base_gongyingshouquanViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "供应商序号")]
        public int GongyingshangID { get; set; }
        [Display(Name = "授权单位序号")]
        public int ShouquanID { get; set; }
        [Display(Name = "授权单位名称")]
        public string Shouquanmingcheng { get; set; }
        [Display(Name = "授权书有效期")]
        [DataType(DataType.Date)]
        public DateTime ShouquanshuYXQ { get; set; }
        [Display(Name = "授权书照片")]
        public string ShouquanshuTP { get; set; }
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

