using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class base_shangpinxxViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "货主序号")]
        public int? HuozhuID { get; set; }
        [Display(Name = "货主的授权序号")]
        public int? HuozhuSQID { get; set; }
        [Display(Name = "商品代码")]
        public string Daima { get; set; }
        [Display(Name = "商品名称")]
        public string Mingcheng { get; set; }
        [Display(Name = "注册证序号")]
        public int? ZhucezhengID { get; set; }
        [Display(Name = "注册证编号")]
        public string ZhucezhengBH { get; set; }
        [Display(Name = "规格")]
        public string Guige { get; set; }
        [Display(Name = "型号")]
        public string Xinghao { get; set; }
        [Display(Name = "单位")]
        public string Danwei { get; set; }
        [Display(Name = "换算率")]
        public float? Huansuanlv { get; set; }
        [Display(Name = "长")]
        public float? Volchang { get; set; }
        [Display(Name = "宽")]
        public float? Volkuan { get; set; }
        [Display(Name = "高")]
        public float? Volgao { get; set; }
        [Display(Name = "产品线序号")]
        public int? Chanpinxian { get; set; }
        [Display(Name = "分类序号")]
        public int? Muluxuhao { get; set; }
        [Display(Name = "管理分类")]
        public int? Guanlifenlei { get; set; }
        [Display(Name = "包装要求")]
        public string Baozhuangyaoqiu { get; set; }
        [Display(Name = "存储条件")]
        public string Cunchutiaojian { get; set; }
        [Display(Name = "生产企业序号")]
        public int? QiyeID { get; set; }
        [Display(Name = "生产企业名称")]
        public string Qiyemingcheng { get; set; }
        [Display(Name = "供应商")]
        public int? GongyingID { get; set; }
        [Display(Name = "供应商的授权序号")]
        public int? GongyingSQID { get; set; }
        [Display(Name = "供应商的销售序号")]
        public int? GongyingXSID { get; set; }
        [Display(Name = "首营状态")]
        public int? Shouying { get; set; }
        [Display(Name = "机动1")]
        public string Col1 { get; set; }
        [Display(Name = "机动2")]
        public string Col2 { get; set; }
        [Display(Name = "机动3")]
        public string Col3 { get; set; }
        [Display(Name = "机动4")]
        public string Col4 { get; set; }
        [Display(Name = "机动5")]
        public string Col5 { get; set; }
        [Display(Name = "机动6")]
        public string Col6 { get; set; }
        [Display(Name = "录入日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "输入人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
        [Display(Name = "是否审查")]
        public bool ShenchaSF { get; set; }
        [Display(Name = "是否经营")]
        public bool JingyinSF { get; set; }
        [Display(Name = "包装单位")]
        public string BaozhuangDW { get; set; }
        [Display(Name = "商品条码")]
        public string ShangpinTM { get; set; }
        [Display(Name = "产地")]
        public string Chandi { get; set; }
        [Display(Name = "商品描述")]
        public string ShangpinMS { get; set; }
        [Display(Name = "温度上限")]
        public float? WenduSX { get; set; }
        [Display(Name = "温度下限")]
        public float? WenduXX { get; set; }
    }
}

