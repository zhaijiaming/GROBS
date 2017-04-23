using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class auth_juesemxViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "角色序号")]
        public int? RoleID { get; set; }
        [Display(Name = "功能序号")]
        public int? FuncID { get; set; }
        [Display(Name = "日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

