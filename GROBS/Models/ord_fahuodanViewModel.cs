using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class ord_fahuodanViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "发货单号")]
        public string ChukudanBH { get; set; }
        [Display(Name = "销售单号")]
        public string KehuDH { get; set; }
        [Display(Name = "订单序号")]
        public int? DDID { get; set; }
        [Display(Name = "订单编号")]
        public string DDBH { get; set; }
        [Display(Name = "运送地址")]
        public string Yunsongdizhi { get; set; }
        [Display(Name = "发货日期")]
        [DataType(DataType.Date)]
        public DateTime? ChukuRQ { get; set; }
        [Display(Name = "联系人")]
        public string Lianxiren { get; set; }
        [Display(Name = "联系电话")]
        public string LianxiDH { get; set; }
        [Display(Name = "备注")]
        public string Beizhu { get; set; }
        [Display(Name = "仓库代码")]
        public string CKCode { get; set; }
        [Display(Name = "运送方式")]
        public string YunsongFS { get; set; }
        [Display(Name = "快递单号")]
        public string Kddanhao { get; set; }
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

