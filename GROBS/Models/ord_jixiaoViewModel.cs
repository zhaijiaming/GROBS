using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class ord_jixiaoViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "客户")]
        public int? KHID { get; set; }
        [Display(Name = "年度")]
        public int? Niandu { get; set; }
        [Display(Name = "月份")]
        public int? Yuefen { get; set; }
        [Display(Name = "指标")]
        public float? Zhibiao { get; set; }
        [Display(Name = "业绩")]
        public float? Yeji { get; set; }
        [Display(Name = "达成率")]
        public float? Dachenglv { get; set; }
        [Display(Name = "返利申请金额")]
        public float? FLSQJE { get; set; }
        [Display(Name = "返利发放金额")]
        public float? FLFFJE { get; set; }
        [Display(Name = "返利是否发放")]
        public bool FafangSF { get; set; }
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

