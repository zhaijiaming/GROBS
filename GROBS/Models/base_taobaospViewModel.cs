using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
            
namespace GROBS.Models
{
    public partial class base_taobaospViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "套包序号")]
        public int? TBID { get; set; }
        [Display(Name = "商品序号")]
        public int? SPID { get; set; }
        [Display(Name = "数量")]
        public float? Shuliang { get; set; }
        [Display(Name = "换算率")]
        public float? HSL { get; set; }
        [Display(Name = "基本单位")]
        public string JBDW { get; set; }
        [Display(Name = "销售单位")]
        public string XSDW { get; set; }
        [Display(Name = "销售价")]
        public float? JiaXS { get; set; }
        [Display(Name = "采购价")]
        public float? JiaCG { get; set; }
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

