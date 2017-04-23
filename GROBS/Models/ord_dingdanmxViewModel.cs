using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class ord_dingdanmxViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "订单序号")]
        public int? DDID { get; set; }
        [Display(Name = "商品序号")]
        public int? SPID { get; set; }
        [Display(Name = "商品代码")]
        public string SPBM { get; set; }
        [Display(Name = "商品名称")]
        public string SPMC { get; set; }
        [Display(Name = "规格")]
        public string Guige { get; set; }
        [Display(Name = "采购数量")]
        public float? CGSL { get; set; }
        [Display(Name = "发货数量")]
        public float? FHSL { get; set; }
        [Display(Name = "销售报价")]
        public float? XSBJ { get; set; }
        [Display(Name = "销售单价")]
        public float? XSDJ { get; set; }
        [Display(Name = "金额")]
        public float? Jine { get; set; }
        [Display(Name = "折扣")]
        public float? Zhekou { get; set; }
        [Display(Name = "折扣率")]
        public float? Zhekoulv { get; set; }
        [Display(Name = "换算率")]
        public float? HSL { get; set; }
        [Display(Name = "换算编码")]
        public string HSBM { get; set; }
        [Display(Name = "基本单位")]
        public string JBDW { get; set; }
        [Display(Name = "销售单位ie")]
        public string XSDW { get; set; }
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

