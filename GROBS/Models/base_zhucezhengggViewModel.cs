using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class base_zhucezhengggViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "注册证序号")]
        public int? ZCZID { get; set; }
        [Display(Name = "规格")]
        public string Guige { get; set; }
        [Display(Name = "说明")]
        public string Memo { get; set; }
        [Display(Name = "机动1")]
        public string Col1 { get; set; }
        [Display(Name = "制单日期")]
        [DataType(DataType.Date)]
        public DateTime MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

