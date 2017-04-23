using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GROBS.Models;
using System.Web.Mvc;
namespace GROBS.App_Code
{
    public static class UrlHelperExtensions
    {
        #region 分页
        /// <summary>
        /// 分页标签
        /// </summary>
        /// <param name="urlHelper"></param>
        /// <param name="pageModel"></param>
        /// <param name="index"></param>
        /// <param name="isCurrentIndex"></param>
        /// <param name="isDisable"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static MvcHtmlString CreatPageLiTag(this UrlHelper urlHelper,
                                                   BasePageModel pageModel,
                                                   int index,
                                                   bool isCurrentIndex = false,
                                                   bool isDisable = true,
                                                   string content = "")
        {

            string url = urlHelper.Action(pageModel.ActionName, new { searchkey = pageModel.SearchKeyWord, index = index });
            string activeClass = !isCurrentIndex ? string.Empty : "class='active'";
            string disableClass = isDisable ? string.Empty : "class='disabled'";
            url = isDisable ? "href='" + url + "'" : string.Empty;
            string contentString = string.IsNullOrEmpty(content) ? index.ToString() : content;

            return new MvcHtmlString("<li " + activeClass + disableClass + "><a " + url + ">" + contentString + "</a></li>");
        }

        #endregion

    }
}