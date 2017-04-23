using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class base_renyuanxxViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "编号")]
        public string Bianhao { get; set; }
        [Display(Name = "姓名")]
        public string Mingcheng { get; set; }
        [Display(Name = "性别")]
        public int? Xingbie { get; set; }
        [Display(Name = "电话")]
        public string Dianhua { get; set; }
        [Display(Name = "生日")]
        [DataType(DataType.Date)]
        public DateTime? Shengri { get; set; }
        [Display(Name = "部门")]
        public string Bumen { get; set; }
        [Display(Name = "职位")]
        public string Zhiwei { get; set; }
        [Display(Name = "职责")]
        public string Zhize { get; set; }
        [Display(Name = "备注")]
        public string Beizhu { get; set; }
        [Display(Name = "机动1")]
        public string Col1 { get; set; }
        [Display(Name = "机动2")]
        public string Col2 { get; set; }
        [Display(Name = "机动3")]
        public string Col3 { get; set; }
        [Display(Name = "录入日期")]
        [DataType(DataType.Date)]
        public DateTime MakeDate { get; set; }
        [Display(Name = "输入人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
        [Display(Name = "用户序号")]
        public int? UserID { get; set; }
    }
}

