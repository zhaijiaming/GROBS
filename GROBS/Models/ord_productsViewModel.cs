using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class ord_productsViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "商品代码")]
        public string Daima { get; set; }
        [Display(Name = "商品名称")]
        public string Mingcheng { get; set; }
        [Display(Name = "规格")]
        public string Guige { get; set; }
        [Display(Name = "数量")]
        public float? Shuliang { get; set; }
        [Display(Name = "单价")]
        public decimal? JiaXS { get; set; }
        [Display(Name = "金额")]
        public decimal? Jine { get; set; }
        [Display(Name = "备注")]
        public string Beizhu { get; set; }

    }
}