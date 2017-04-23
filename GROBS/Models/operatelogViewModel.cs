using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class operatelogViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "模块序号")]
        public string ModelID { get; set; }
        [Display(Name = "日志信息")]
        public string OpInfo { get; set; }
        [Display(Name = "操作人员")]
        public string OpMan { get; set; }
        [Display(Name = "操作日期")]
        [DataType(DataType.Date)]
        public DateTime OpDate { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

