using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GROBS.EFModels;
namespace GROBS.Models
{
    public partial class auth_authorizeViewModel
    {
        [Display(Name = "角色编号")]
        public int RoleID { get; set; }
        [Display(Name = "角色名称")]
        public string RoleName { get; set; }
        [Display(Name = "已获授权")]
        public IList<auth_quanxian> AuthorizedUser { get; set; }
        [Display(Name = "所有用户")]
        public IList<userinfo> AllUser { get; set; }
    }
}