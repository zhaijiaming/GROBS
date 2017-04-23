using GROBS.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class base_shangpinzczViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "注册证编号")]
        public string Bianhao { get; set; }
        [Display(Name = "商品名称")]
        public string Mingcheng { get; set; }
        [Display(Name = "规格")]
        public string Guige { get; set; }
        [Display(Name = "产品标准")]
        public string Bianzhun { get; set; }
        [Display(Name = "注册证有效期")]
        [DataType(DataType.Date)]
        public DateTime? ZhucezhengYXQ { get; set; }
        [Display(Name = "注册证照片")]
        public string ZhucezhengTP { get; set; }
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
        [Display(Name = "是否失效")]
        public bool ShixiaoSF { get; set; }
        [Display(Name = "中文编号")]
        public string BianhaoZW { get; set; }
        [Display(Name = "产地")]
        public string Chandi { get; set; }
        [Display(Name = "生产企业")]
        public int ShengchanqiyeID { get; set; }
        [Display(Name = "适用范围")]
        public string ShiyongFW { get; set; }
        [Display(Name = "注册代理")]
        public string ZhuceDL { get; set; }
        [Display(Name = "服务机构")]
        public string ShouhuoJG { get; set; }
        [Display(Name = "备注")]
        public string Beizhu { get; set; }
        [Display(Name = "批准日期")]
        [DataType(DataType.Date)]
        public DateTime? PizhunRQ { get; set; }
    }
}

