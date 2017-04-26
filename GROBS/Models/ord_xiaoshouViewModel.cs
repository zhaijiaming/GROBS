using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class ord_xiaoshouViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "订单序号")]
        public int? DDID { get; set; }
        [Display(Name = "订单编号")]
        public string DDBH { get; set; }
        [Display(Name = "销售单号")]
        public string XSDH { get; set; }
        [Display(Name = "快递单号")]
        public string KDDH { get; set; }
        [Display(Name = "发货日期")]
        [DataType(DataType.Date)]
        public DateTime? FahuoRQ { get; set; }
        [Display(Name = "客服人员")]
        public string Kefu { get; set; }
        [Display(Name = "客服电话")]
        public string KefuDH { get; set; }
        [Display(Name = "是否整单")]
        public bool? ZhengdanSF { get; set; }
        [Display(Name = "是否关闭")]
        public bool GuanbiSF { get; set; }
        [Display(Name = "机动1")]
        public string Col1 { get; set; }
        [Display(Name = "制单日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
        [Display(Name = "快递公司")]
        public string KuaidiGS { get; set; }
        [Display(Name = "运送方式")]
        public string FayunFS { get; set; }
    }
}

