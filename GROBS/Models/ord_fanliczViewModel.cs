using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class ord_fanliczViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "客户序号")]
        public int? KHID { get; set; }
        [Display(Name = "充值金额")]
        public float? CZJE { get; set; }
        [Display(Name = "发放月份")]
        public string FFYF { get; set; }
        [Display(Name = "是否可用")]
        public bool KYSF { get; set; }
        [Display(Name = "备注")]
        public string BZ { get; set; }
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

