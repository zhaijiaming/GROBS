using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class ord_dingdanViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "编号")]
        public string Bianhao { get; set; }
        [Display(Name = "客户")]
        public int? KHID { get; set; }
        [Display(Name = "产品线")]
        public int? CPXID { get; set; }
        [Display(Name = "采购类型")]
        public int? CGLX { get; set; }
        [Display(Name = "客户单号")]
        public string KehuDH { get; set; }
        [Display(Name = "下单日期")]
        [DataType(DataType.Date)]
        public DateTime? XiadanRQ { get; set; }
        [Display(Name = "联系人")]
        public string Lianxiren { get; set; }
        [Display(Name = "联系电话")]
        public string LianxiDH { get; set; }
        [Display(Name = "送货地址")]
        public string SonghuoDZ { get; set; }
        [Display(Name = "客服人员")]
        public int? OPID { get; set; }
        [Display(Name = "审核人员")]
        public int? ACCID { get; set; }
        [Display(Name = "审核通过")]
        public bool? ShenheTG { get; set; }
        [Display(Name = "采购总数")]
        public float? ZongshuCG { get; set; }
        [Display(Name = "总金额")]
        public float? Zongjine { get; set; }
        [Display(Name = "是否结束")]
        public bool? JieshuSF { get; set; }
        [Display(Name = "状态")]
        public int? Zhuangtai { get; set; }
        [Display(Name = "备注")]
        public string Beizhu { get; set; }
        [Display(Name = "机动1")]
        public string Col1 { get; set; }
        [Display(Name = "机动2")]
        public string Col2 { get; set; }
        [Display(Name = "机动3")]
        public string Col3 { get; set; }
        [Display(Name = "制单日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

