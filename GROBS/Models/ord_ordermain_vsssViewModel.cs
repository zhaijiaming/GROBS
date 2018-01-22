using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GROBS.EFModels;
namespace GROBS.Models
{
    public partial class ord_ordermain_vsssViewModel
    {
        public int ID { get; set; }
        public string Bianhao { get; set; }
        public int KHID { get; set; }
        public string KehuDM { get; set; }
        public string KehuMC { get; set; }
        public Nullable<int> CPXID { get; set; }
        public string Mingcheng { get; set; }
        public Nullable<int> CGLX { get; set; }
        public Nullable<System.DateTime> XiadanRQ { get; set; }
        public string KehuDH { get; set; }
        public string Lianxiren { get; set; }
        public string LianxiDH { get; set; }
        public string SonghuoDZ { get; set; }
        public Nullable<bool> ShenheTG { get; set; }
        public Nullable<float> ZongshuCG { get; set; }
        public Nullable<decimal> Zongjine { get; set; }
        public Nullable<decimal> ZhekouJE { get; set; }
        public Nullable<bool> JieshuSF { get; set; }
        public Nullable<int> Zhuangtai { get; set; }
        public string Beizhu { get; set; }
        public string FKPZ { get; set; }
        public Nullable<int> HH { get; set; }
        public string SPBM { get; set; }
        public float CGSL { get; set; }
        public float QHSL { get; set; }
        public float XSDJ { get; set; }
        public float Zhekou { get; set; }
        public decimal Jine { get; set; }
        public int DDID { get; set; }
    }
}