using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class base_kaipiaoxxViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "客户序号")]
        public int? KehuID { get; set; }
        [Display(Name = "抬头")]
        public string Taitou { get; set; }
        [Display(Name = "税号")]
        public string Shuihao { get; set; }
        [Display(Name = "开户行")]
        public string Kiahuhang { get; set; }
        [Display(Name = "账号")]
        public string Zhanghao { get; set; }
        [Display(Name = "税率")]
        public float? Shuilv { get; set; }
        [Display(Name = "折扣率")]
        public float? Zhekou { get; set; }
        [Display(Name = "递送地址")]
        public string DisongDZ { get; set; }
        [Display(Name = "收件人")]
        public string Shoujianren { get; set; }
        [Display(Name = "联系电话")]
        public string LianxiDH { get; set; }
        [Display(Name = "是否无效")]
        public bool WuxiaoSF { get; set; }
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

