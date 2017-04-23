using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class base_qixiemuluViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "编号")]
        public string Bianhao { get; set; }
        [Display(Name = "名称")]
        public string Mingcheng { get; set; }
        [Display(Name = "描述")]
        public string Miaoshu { get; set; }
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
        public bool? IsDelete { get; set; }
        [Display(Name = "管理分类")]
        public int? GuanliFL { get; set; }
    }
}

