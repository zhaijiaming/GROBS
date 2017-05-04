using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class ord_qianhuoViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "明细序号")]
        public int? MXID { get; set; }
        [Display(Name = "欠货数量")]
        public float? QHSL { get; set; }
        [Display(Name = "登记日期")]
        [DataType(DataType.Date)]
        public DateTime? DJRQ { get; set; }
        [Display(Name = "是否采购")]
        public bool CaigouSF { get; set; }
        [Display(Name = "采购编号")]
        public string CaigouBH { get; set; }
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

