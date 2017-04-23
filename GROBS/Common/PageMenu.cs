using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GROBS.Common
{
    public static class PageMenu
    {
        public static string Current_Page { get; set; }
        public static string Current_Control { get; set; }
        public static string Current_Model { get; set; }

        public static void Set(string currentpage, string currentcontrol, string currentmodel)
        {
            Current_Page = currentpage;
            Current_Model = currentmodel;
            Current_Control = currentcontrol;
        }
        public static string Clear()
        {
            Current_Control = "";
            Current_Model = "";
            Current_Page = "";
            return "";
        }
    }
}