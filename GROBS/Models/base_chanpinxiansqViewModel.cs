using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class base_chanpinxiansqViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "经销商序号")]
        public int? JXSID { get; set; }
        [Display(Name = "产品线序号")]
        public int? CPXID { get; set; }
        [Display(Name = "经销商代码")]
        public string JXSDM { get; set; }
        [Display(Name = "产品线代码")]
        public string CPXDM { get; set; }
        [Display(Name = "是否停用")]
        public bool TingyongSF { get; set; }
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

