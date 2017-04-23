using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class userinfoViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Required]
        [Display(Name = "账号")]
        public string Account { get; set; }
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [Required]
        [Display(Name = "全名")]
        public string FullName { get; set; }
        [Display(Name = "员工号")]
        public int? EmployeeID { get; set; }
        [Display(Name = "类型")]
        public int? AccountType { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        [Display(Name = "状态")]
        public int? Status { get; set; }
        [Required]
        [Display(Name = "输入人")]
        public string InputMan { get; set; }
        [Display(Name = "输入日期")]
        [DataType(DataType.Date)]
        public DateTime? InputDate { get; set; }
        [Display(Name = "修改人")]
        public string ModifyMan { get; set; }
        [Display(Name = "修改日期")]
        [DataType(DataType.Date)]
        public DateTime? ModifyDate { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

