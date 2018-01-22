using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class ord_fanlixfViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "订单序号")]
        public int? DDID { get; set; }
        [Display(Name = "客户序号")]
        public int? KHID { get; set; }
        [Display(Name = "消费金额")]
        public decimal? XFJE { get; set; }
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

