using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class base_quyuViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Required(ErrorMessage = "编号不能为空")]
        [Display(Name = "编号")]
        public string Bianhao { get; set; }
        [Required(ErrorMessage = "名称不能为空")]
        [Display(Name = "名称")]
        public string Mingcheng { get; set; }
        [Display(Name = "描述")]
        public string Miaoshu { get; set; }
        [Display(Name = "机动")]
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

