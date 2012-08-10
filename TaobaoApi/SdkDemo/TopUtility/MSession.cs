using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TopCore
{
    public static class MSession
    {
        public static bool Set(string key, object val)
        {
            var result = false;
            var context = HttpContext.Current;
            if (context != null && context.Session != null)
            {
                try
                {
                    context.Session[key] = val;
                    result = true;
                }
                catch { }
            }
            return result;
        }

        public static object Get(string key)
        {
            object result = null;
            var context = HttpContext.Current;
            if (context != null && context.Session != null)
            {
                try
                {
                    result = context.Session[key];
                }
                catch { }
            }
            return result;
        }

        public static bool Remove(string key)
        {
            var result = false;
            var context = HttpContext.Current;
            if (context != null && context.Session != null)
            {
                try
                {
                    context.Session.Remove(key);
                    result = true;
                }
                catch { }
            }
            return result;
        }

        public static void RemoveAll()
        {
            var context = HttpContext.Current;
            if (context != null && context.Session != null)
            {
                try
                {
                    context.Session.RemoveAll();
                }
                catch { }
            }
        }
    }
}
