using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class base_huobiViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "编号")]
        [Required]
        public string Bianhao { get; set; }
        [Display(Name = "名称")]
        [Required]
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

