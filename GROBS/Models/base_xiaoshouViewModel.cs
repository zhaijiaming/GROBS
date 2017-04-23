using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class base_xiaoshouViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "类别")]
        public int Leibie { get; set; }
        [Display(Name = "货主")]
        [Required]
        public int? ShouquanID { get; set; }
        [Display(Name = "授权单位名称")]
        public string Shouquanmingcheng { get; set; }
        [Display(Name = "姓名")]
        [Required]
        public string Xingming { get; set; }
        [Display(Name = "电话")]
        public string Dianhua { get; set; }
        [Display(Name = "身份证编号")]
        [Required(ErrorMessage = "身份证编号不能为空")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "身份证编号格式不对")]
        public string ShenfenzhengBH { get; set; }
        [Display(Name = "身份证正面照片")]
        public string Shenfenzheng0TP { get; set; }
        [Display(Name = "身份证背面照片")]
        public string Shenfenzheng1TP { get; set; }
        [Display(Name = "授权书有效期")]
        [DataType(DataType.Date)]
        public DateTime? ShouquanshuYXQ { get; set; }
        [Display(Name = "授权书照片")]
        public string ShouquanshuTP { get; set; }
        [Display(Name = "首营状态")]
        public int? Shouying { get; set; }
        [Display(Name = "机动1")]
        public string Col1 { get; set; }
        [Display(Name = "机动2")]
        public string Col2 { get; set; }
        [Display(Name = "机动3")]
        public string Col3 { get; set; }
        [Display(Name = "机动4")]
        public string Col4 { get; set; }
        [Display(Name = "机动5")]
        public string Col5 { get; set; }
        [Display(Name = "机动6")]
        public string Col6 { get; set; }
        [Display(Name = "录入日期")]
        [DataType(DataType.Date)]
        public DateTime MakeDate { get; set; }
        [Display(Name = "录入人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

