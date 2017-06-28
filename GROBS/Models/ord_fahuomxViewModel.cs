using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class ord_fahuomxViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "发货序号")]
        public int? ChukuID { get; set; }
        [Display(Name = "商品代码")]
        public string ShangpinDM { get; set; }
        [Display(Name = "商品名称")]
        public string ShangpinMC { get; set; }
        [Display(Name = "注册证")]
        public string Zhucezheng { get; set; }
        [Display(Name = "规格")]
        public string Guige { get; set; }
        [Display(Name = "批号")]
        public string Pihao { get; set; }
        [Display(Name = "序列码")]
        public string Xuliehao { get; set; }
        [Display(Name = "生产日期")]
        [DataType(DataType.Date)]
        public DateTime? ShengchanRQ { get; set; }
        [Display(Name = "失效日期")]
        [DataType(DataType.Date)]
        public DateTime? ShixiaoRQ { get; set; }
        [Display(Name = "发货数量")]
        public float? ChukuSL { get; set; }
        [Display(Name = "单位")]
        public string Danwei { get; set; }
        [Display(Name = "换算率")]
        public float? Huansuanlv { get; set; }
        [Display(Name = "套包号")]
        public string Taobaohao { get; set; }
        [Display(Name = "备注")]
        public string Beizhu { get; set; }
        [Display(Name = "机动1")]
        public string Col1 { get; set; }
        [Display(Name = "制单人")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "制单日期")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

