using System;
using System.Collections.Generic;
using System.Text;

namespace CCmall.Common.Helper
{
    public static class ObjHelper
    {
        public static int ToInt(this object obj)
        {
            var ret = 0;
            if (obj == null)
            {
                return ret;
            }
            if (obj != null && obj != DBNull.Value && int.TryParse(obj.ToString(), out ret))
            {
                return ret;
            }
            return ret;
        }
        public static string ToStr(this object obj)
        {
            var ret = string.Empty;
            if (obj == null)
            {
                return ret;
            }
            if (obj != null && obj != DBNull.Value)
            {
                return obj.ToString();
            }
            return ret;
        }
        public static bool ToBool(this object obj)
        {
            var ret = false;
            if (obj == null)
            {
                return ret;
            }
            if (obj != null && obj != DBNull.Value && bool.TryParse(obj.ToString(), out ret))
            {
                return ret;
            }
            return ret;
        }
    }
}
