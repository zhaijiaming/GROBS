using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class ord_fanliViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "客户")]
        public int? KHID { get; set; }
        [Display(Name = "总额")]
        public decimal? Zonge { get; set; }
        [Display(Name = "可用")]
        public decimal? Keyong { get; set; }
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

