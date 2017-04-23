using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace GROBS.Models
{
    /// <summary>
    /// 查询条件
    /// </summary>
    [Serializable]
    public class SearchConditionModel
    {
        public string ItemTitle { get; set; }//项目标题
        public string ItemCode { get; set; }//项目代码
        public string ItemValue { get; set; }//项目值
        public string ItemType { get; set; }//项目类型
        public string ItemOpValue { get; set; }//项目操作符
        public string ItemJion { get; set; }//项目连接符
        public string ItemShowName { get; set; }//调用项目显示名
        public SearchConditionModel(string title, string code, string type, string value, string join, string showname)
        {
            ItemTitle = title;
            ItemCode = code;
            ItemType = type;
            ItemValue = value;
            ItemJion = join;
            ItemShowName = showname;
        }
        public SearchConditionModel() { }
    }
}