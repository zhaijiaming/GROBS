using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROBS.Models
{
    public partial class base_gongyingshangViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "代码")]
        [Required(ErrorMessage = "代码不能为空")]
        public string Daima { get; set; }
        [Display(Name = "名称")]
        [Required(ErrorMessage = "名称不能为空")]
        public string Mingcheng { get; set; }
        [Display(Name = "营业执照编号")]
        [Required(ErrorMessage = "营业执照编号不能为空")]
        public string YingyezhizhaoBH { get; set; }
        [Display(Name = "营业执照有效期")]
        [DataType(DataType.Date)]
        //[Required(ErrorMessage = "营业执照有效期不能为空")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime YingyezhizhaoYXQ { get; set; }
        [Display(Name = "营业执照照片")]
        public string YingyezhizhaoTP { get; set; }
        [Display(Name = "经营许可编号")]
        //[Required(ErrorMessage = "经营许可编号不能为空")]
        public string JingyingxukeBH { get; set; }
        [Display(Name = "经营许可有效期")]
        [DataType(DataType.Date)]
        //[Required(ErrorMessage = "经营许可有效期不能为空")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JingyingxukeYXQ { get; set; }
        [Display(Name = "经营许可照片")]
        public string JingyingxukeTP { get; set; }
        [Display(Name = "经营范围")]
        public string Jingyingfanwei { get; set; }
        [Display(Name = "经营范围简码")]
        public string JingyingfanweiDM { get; set; }
        [Display(Name = "首营状态")]
        public int Shouying { get; set; }
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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime MakeDate { get; set; }
        [Display(Name = "输入人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
        [Display(Name = "是否审查")]
        public bool ShenchaSF { get; set; }
        [Display(Name = "是否合作")]
        public bool HezuoSF { get; set; }
        [Display(Name = "备案编号")]
        public string BeianBH { get; set; }
        [Display(Name = "备案有效期")]
        [DataType(DataType.Date)]
        public DateTime? BeianYXQ { get; set; }
        [Display(Name = "备案批准日期")]
        [DataType(DataType.Date)]
        public DateTime? BeianPZRQ { get; set; }
        [Display(Name = "备案发证机关")]
        public string BeianFZJG { get; set; }
        [Display(Name = "备案文件")]
        public string BeianTP { get; set; }
        [Display(Name = "经营许可批准日期")]
        [DataType(DataType.Date)]
        public DateTime? XukePZRQ { get; set; }
        [Display(Name = "经营许可发证机关")]
        public string XukeFZJG { get; set; }
    }
}

