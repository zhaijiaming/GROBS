using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class auth_quanxianViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "角色序号")]
        public int? JSID { get; set; }
        [Display(Name = "人员序号")]
        public int? RYID { get; set; }
        [Display(Name = "失效日期")]
        [DataType(DataType.Date)]
        public DateTime? SXDate { get; set; }
        [Display(Name = "是否关闭")]
        public bool? GuanbiSF { get; set; }
        [Display(Name = "制单日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

