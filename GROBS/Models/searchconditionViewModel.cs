using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class searchconditionViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "用户序号")]
        public int UserID { get; set; }
        [Display(Name = "条件信息")]
        public string ConditionInfo { get; set; }
        [Display(Name = "页面简码")]
        public string PageBrief { get; set; }
        [Display(Name = "修改日期")]
        [DataType(DataType.Date)]
        public DateTime ModifyDate { get; set; }
        [Display(Name = "查询标题")]
        public string ConditionTitle { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

