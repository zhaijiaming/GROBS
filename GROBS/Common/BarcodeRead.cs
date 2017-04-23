using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GROBS.Common
{
    public static class BarcodeRead
    {
        public static string BatchCode(string batchnum)
        {
            string _body = "";
            if (string.IsNullOrEmpty(batchnum))
                return "";
            string _head = batchnum.Substring(0, 2);
            if (_head == "17")
            {
                if (batchnum.Substring(8, 2) == "10")
                    _body = batchnum.Substring(10);
            }
            else
                _body = batchnum;
            return _body;
        }
        public static string SerialNumber(string serailnum)
        {
            string _body = "";

            if (string.IsNullOrEmpty(serailnum))
                return "";
            string _head = serailnum.Substring(0, 2);
            if (_head == "21")
                _body = serailnum.Substring(2);
            else
            {
                if (_head == "17")
                {
                    if (serailnum.Substring(8, 2) == "21")
                        _body = serailnum.Substring(10);
                }
                else
                    _body = serailnum;
            }
            return _body;
        }
    }
}