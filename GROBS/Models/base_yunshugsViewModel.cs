using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class base_yunshugsViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "简称")]
        public string Jiancheng { get; set; }
        [Display(Name = "名称")]
        public string Mingcheng { get; set; }
        [Display(Name = "描述")]
        public string Miaoshu { get; set; }
        [Display(Name = "类型")]
        public int? Leixing { get; set; }
        [Display(Name = "联系人")]
        public string Lianxiren { get; set; }
        [Display(Name = "联系电话")]
        public string LianxiDH { get; set; }
        [Display(Name = "负责人")]
        public string Fuzeren { get; set; }
        [Display(Name = "负责人电话")]
        public string FuzeDH { get; set; }
        [Display(Name = "结账方式")]
        public string JiezhangFS { get; set; }
        [Display(Name = "营业执照")]
        public string YingyeZZ { get; set; }
        [Display(Name = "执照图片")]
        public string ZhizhaoTP { get; set; }
        [Display(Name = "机动1")]
        public string Col1 { get; set; }
        [Display(Name = "机动2")]
        public string Col2 { get; set; }
        [Display(Name = "机动3")]
        public string Col3 { get; set; }
        [Display(Name = "制单日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

