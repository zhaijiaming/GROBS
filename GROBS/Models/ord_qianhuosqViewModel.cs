using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class ord_qianhuosqViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "申请编号")]
        public string ShenqinBH { get; set; }
        [Display(Name = "是否发送")]
        public bool FasongSF { get; set; }
        [Display(Name = "是否接收")]
        public bool JieshouSF { get; set; }
        [Display(Name = "接收人")]
        public int? Jieshouren { get; set; }
        [Display(Name = "接收时间")]
        [DataType(DataType.Date)]
        public DateTime? JieshouSJ { get; set; }
        [Display(Name = "机动1")]
        public string Col1 { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
        [Display(Name = "制单日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
    }
}

