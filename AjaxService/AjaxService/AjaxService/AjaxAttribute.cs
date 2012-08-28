using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjaxService
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AjaxAttribute : Attribute
    {
        public bool EnableAjax { get; set; }

        public AjaxAttribute()
        {

        }

    }
}