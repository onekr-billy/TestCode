using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public static class CvHelper
    {
        /// <summary>
        /// 数据类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static T To<T>(object obj, params T[] def)
        {
            Type _t = typeof(T);
            bool IsError = false;
            object _obj = null;
            try
            {
                if (_t.IsEnum)
                {
                    _obj = Enum.Parse(typeof(T), obj.ToString(), true);
                }
                else
                {
                    _obj = Convert.ChangeType(obj, _t);
                }
            }
            catch
            {
                IsError = true;
            }
            finally
            {
                if (IsError || _obj == null)
                {
                    if (def != null && def.Length > 0)
                        _obj = def[0];
                    else
                        _obj = default(T);
                }
            }
            return (T)_obj;
        }

        /// <summary>
        /// 转换为数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(object obj)
        {
            Type _t = typeof(T);
            bool IsError = false;
            object _obj = null;
            try
            {
                string[] arrays = Convert.ToString(obj).Split(new char[] { ',', '|', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (arrays.Length > 0)
                {
                    _obj = Array.ConvertAll<string, T>(arrays, s =>
                    {
                        return (T)Convert.ChangeType(s, _t);
                    });
                }
            }
            catch
            {
                IsError = true;
            }
            finally
            {
                if (IsError || _obj == null)
                {
                    _obj = default(T);
                }
            }
            return (T[])_obj;
        }

    }
}
