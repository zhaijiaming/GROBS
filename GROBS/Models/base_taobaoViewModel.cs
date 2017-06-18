using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class base_taobaoViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "供应商")]
        public int? GYSID { get; set; }
        [Display(Name = "产品线")]
        public int? CPXID { get; set; }
        [Display(Name = "套包号")]
        public string Daima { get; set; }
        [Display(Name = "套包名称")]
        public string Mingcheng { get; set; }
        [Display(Name = "套包描述")]
        public string Miaoshu { get; set; }
        [Display(Name = "销售价")]
        public float? JiaXS { get; set; }
        [Display(Name = "采购价")]
        public float? JiaCG { get; set; }
        [Display(Name = "销售单位")]
        public string XSDW { get; set; }
        [Display(Name = "是否停用")]
        public bool TingyongSF { get; set; }
        [Display(Name = "是否受控")]
        public bool KongzhiSF { get; set; }
        [Display(Name = "机动1")]
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

