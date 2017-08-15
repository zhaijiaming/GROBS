using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class base_chanpinxianViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "货主序号")]
        public int Huozhuxuhao { get; set; }
        [Display(Name = "供应商ID")]
        public int GYSID { get; set; }
        [Display(Name = "名称")]
        public string Mingcheng { get; set; }
        [Display(Name = "说明")]
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
        public bool IsDelete { get; set; }
        [Display(Name = "客服序号")]
        public int KFID { get; set; }
        [Display(Name = "产品线代码")]
        public string CPXDM { get; set; }
        [Display(Name = "客服电话")]
        public string KFDH { get; set; }
        [Display(Name = "客服QQ")]
        public string KFQQ { get; set; }
    }
}

