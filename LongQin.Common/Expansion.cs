using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LongQin.Common
{
    /// <summary>
    /// 封装Simple扩展方法。
    /// </summary>
    public static class Expansion
    {
        #region SortIndexOf
        /// <summary>
        /// 检索一个对象在一个"升序"集合中的位置或最合适插入的位置。
        /// </summary>
        /// <typeparam name="T">集合内基于System.IComparable&l;T>的元素类型。</typeparam>
        /// <param name="list">升序集合对象。</param>
        /// <param name="value">要检索的目标值。</param>
        /// <returns>对象在升序集合中的位置或最合适插入的位置。</returns>
        public static int SortIndexOf<T>(this IList<T> list, T value) where T : IComparable<T>
        {
            int idx = (int)Math.Ceiling(list.Count / 2.0);
            double kc = list.Count;
            if (kc.Equals(1))
                idx = list[0].CompareTo(value) < 0 ? 1 : 0;
            else
                for (int sp = 4; idx < kc;)
                {
                    var cp = list[idx].CompareTo(value);
                    if (cp.Equals(0) || (idx == 0 && cp > 0))
                    {
                        break;
                    }
                    else if (idx > 0 && cp > 0 && list[idx - 1].CompareTo(value) <= 0)
                    {
                        if (list[idx - 1].CompareTo(value) == 0)
                            idx -= 1;
                        break;
                    }
                    if (cp < 0)
                    {
                        idx += (int)Math.Ceiling(kc / sp);
                        sp = sp << 1;
                        if (idx > kc || (idx == kc && list[idx - 1].CompareTo(value) >= 0))
                            idx = (int)kc - 1;
                    }
                    else if (cp > 0)
                    {
                        idx -= (int)Math.Ceiling(kc / sp);
                        sp = sp << 1;
                        if (idx < 0)
                            idx = 0;
                    }
                }
            return idx;
        }
        /// <summary>
        /// 检索一个对象在一个[降序]集合中的位置或最合适插入的位置。
        /// </summary>
        /// <typeparam name="T">集合内基于System.IComparable&l;T>的元素类型。</typeparam>
        /// <param name="list">升序集合对象。</param>
        /// <param name="value">要检索的目标值。</param>
        /// <returns>对象在升序集合中的位置或最合适插入的位置。</returns>
        public static int SortDescIndexOf<T>(this IList<T> list, T value) where T : IComparable<T>
        {
            int idx = (int)Math.Ceiling(list.Count / 2.0);
            double kc = list.Count;
            if (kc.Equals(1))
                idx = list[0].CompareTo(value) > 0 ? 1 : 0;
            else
                for (int sp = 4; idx < kc;)
                {
                    var cp = list[idx].CompareTo(value);
                    if (cp.Equals(0) || (idx == 0 && cp < 0))
                    {
                        break;
                    }
                    else if (idx > 0 && cp < 0 && list[idx - 1].CompareTo(value) >= 0)
                    {
                        if (list[idx - 1].CompareTo(value) == 0)
                            idx -= 1;
                        break;
                    }
                    if (cp > 0)
                    {
                        idx += (int)Math.Ceiling(kc / sp);
                        sp = sp << 1;
                        if (idx > kc || (idx == kc && list[idx - 1].CompareTo(value) <= 0))
                            idx = (int)kc - 1;
                    }
                    else if (cp < 0)
                    {
                        idx -= (int)Math.Ceiling(kc / sp);
                        sp = sp << 1;
                        if (idx < 0)
                            idx = 0;
                    }
                }
            return idx;
        }
        /// <summary>
        /// 根据指定的比对委托检索一个对象在一个[升序]集合中的位置或最合适插入的位置。
        /// </summary>
        /// <typeparam name="TElement">集合元素类型</typeparam>
        /// <typeparam name="TComparer">用于比较的目标类型</typeparam>
        /// <param name="list">集合对象</param>
        /// <param name="value">要比较的目标对象</param>
        /// <param name="compareFunc">用于比较操作的方法委托</param>
        /// <returns></returns>
        public static int SortIndexOf<TElement, TComparer>(this IList<TElement> list, TComparer value, Func<TElement, TComparer, int> compareFunc)
        {
            int idx = (int)Math.Ceiling(list.Count / 2.0);
            double kc = list.Count;
            if (kc.Equals(1))
                idx = compareFunc(list[0], value) < 0 ? 1 : 0;
            else
                for (int sp = 4; idx < kc;)
                {
                    var cp = compareFunc(list[idx], value);
                    if (cp.Equals(0) || (idx == 0 && cp > 0))
                    {
                        break;
                    }
                    else if (idx > 0 && cp > 0 && compareFunc(list[idx - 1], value) <= 0)
                    {
                        if (compareFunc(list[idx - 1], value) == 0)
                            idx -= 1;
                        break;
                    }
                    if (cp < 0)
                    {
                        idx += (int)Math.Ceiling(kc / sp);
                        sp = sp << 1;
                        if (idx > kc || (idx == kc && compareFunc(list[idx - 1], value) >= 0))
                            idx = (int)kc - 1;
                    }
                    else if (cp > 0)
                    {
                        idx -= (int)Math.Ceiling(kc / sp);
                        sp = sp << 1;
                        if (idx < 0)
                            idx = 0;
                    }
                }
            return idx;
        }
        /// <summary>
        /// 根据指定的比对委托检索一个对象在一个[降序]集合中的位置或最合适插入的位置。
        /// </summary>
        /// <typeparam name="TElement">集合元素类型</typeparam>
        /// <typeparam name="TComparer">用于比较的目标类型</typeparam>
        /// <param name="list">集合对象</param>
        /// <param name="value">要比较的目标对象</param>
        /// <param name="compareFunc">用于比较操作的方法委托</param>
        /// <returns></returns>
        public static int SortDescIndexOf<TElement, TComparer>(this IList<TElement> list, TComparer value, Func<TElement, TComparer, int> compareFunc)
        {
            int idx = (int)Math.Ceiling(list.Count / 2.0);
            double kc = list.Count;
            if (kc.Equals(1))
                idx = compareFunc(list[0], value) > 0 ? 1 : 0;
            else
                for (int sp = 4; idx < kc;)
                {
                    var cp = compareFunc(list[idx], value);
                    if (cp.Equals(0) || (idx == 0 && cp < 0))
                    {
                        break;
                    }
                    else if (idx > 0 && cp > 0 && compareFunc(list[idx - 1], value) <= 0)
                    {
                        if (compareFunc(list[idx - 1], value) == 0)
                            idx -= 1;
                        break;
                    }
                    if (cp < 0)
                    {
                        idx += (int)Math.Ceiling(kc / sp);
                        sp = sp << 1;
                        if (idx > kc || (idx == kc && compareFunc(list[idx - 1], value) <= 0))
                            idx = (int)kc - 1;
                    }
                    else if (cp > 0)
                    {
                        idx -= (int)Math.Ceiling(kc / sp);
                        sp = sp << 1;
                        if (idx < 0)
                            idx = 0;
                    }
                }
            return idx;
        }
        #endregion

        #region 字符串扩展
        /// <summary>
        /// 根据一个正则从目标字符串中找出需要的部分。
        /// </summary>
        /// <param name="s">需要被查找的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="defValue">匹配失败的默认值</param>
        /// <returns></returns>
        public static string Match(this string s, string pattern, string defValue = "")
        {
            var mat = Regex.Match(s, pattern);
            if (mat.Success)
            {
                if (mat.Groups.Count > 1)
                    return mat.Groups[1].Value;
                else
                    return mat.Value;
            }
            return defValue;
        }

        /// <summary>
        /// 将多个连续的换行符合并为一个换行符。
        /// </summary>
        /// <param name="s">要处理的字符串</param>
        /// <returns></returns>
        public static string UnityWrap(this string s)
        {
            return System.Text.RegularExpressions.Regex.Replace(s, @"(\r\n|\n){1,256}", "$1");
        }

        /// <summary>
        /// 清空字符串中的HTML。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static string EmptyHtml(this string s)
        {
            s = System.Text.RegularExpressions.Regex.Replace(s, @"\<br ?\/\>", Environment.NewLine, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return System.Text.RegularExpressions.Regex.Replace(s, @"<[^>]+>", "");
        }

        #region 转换回车与换行
        /// <summary>
        /// 转换回车与换行
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns></returns>
        public static string ToHtml(this string str)
        {
            if (string.IsNullOrEmpty(str)) return "";
            string tmp = Regex.Replace(str, @"(?:\r\n)|\n", "<br />", RegexOptions.IgnoreCase);
            tmp = Regex.Replace(tmp, @"\r", "", RegexOptions.IgnoreCase);
            tmp = Regex.Replace(tmp, @"  ", "　", RegexOptions.IgnoreCase);

            return tmp;
        }
        #endregion

        /// <summary>
        /// 检测对象是否为Null。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// 检测对象是否不为Null。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        /// <summary>
        /// 指示当前 System.String 对象是 null 还是 System.String.Empty 字符串。
        /// </summary>
        /// <param name="s">System.String 引用。</param>
        /// <returns>如果对象为 null 或空字符串 ("")，则为 true；否则为 false。</returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 确定此字符串是否与指定的 System.String 对象是否表示忽略大小写的相同内容。
        /// </summary>
        /// <param name="s">原字符串</param>
        /// <param name="value">要用于比较的目标字符串</param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string s, string value)
        {
            return s != null && s.Equals(value, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 确定此字符串的开头忽略大小写后是否与指定的字符串匹配。
        /// </summary>
        /// <param name="s">原字符串</param>
        /// <param name="value">要用于比较的目标字符串</param>
        /// <returns></returns>
        public static bool StartsWithIgnoreCase(this string s, string value)
        {
            return s != null && s.StartsWith(value, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 确定此字符串的末尾忽略大小写后是否与指定的字符串匹配。
        /// </summary>
        /// <param name="s">原字符串</param>
        /// <param name="value">要用于比较的目标字符串</param>
        /// <returns></returns>
        public static bool EndsWithIgnoreCase(this string s, string value)
        {
            return s != null && s.EndsWith(value, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="input">格式化对象</param>
        /// <param name="formatString">格式化字符串</param>
        /// <param name="defValue">对象默认值</param>
        /// <returns>格式化后的字符串</returns>
        public static string StringFormat(this object input, string formatString, string defValue)
        {
            return string.Format(formatString, input.ToStringOrDefault(defValue));
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="input">格式化对象</param>
        /// <param name="formatString">格式化字符串</param>
        /// <param name="defValue">对象默认值</param>
        /// <returns>格式化后的字符串</returns>
        public static string StringFormat(this object input, string formatString)
        {
            return input.StringFormat(formatString, "");
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="input">格式化对象</param>
        /// <param name="format">字符串格式</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatTo(this object input, string format)
        {
            if (format.IsNullOrEmpty())
                return input.ToStringOrDefault();
            return string.Format(string.Format(@"{{0:{0}}}", format), input);
        }
        #endregion

        #region Object Ext
        /// <summary>
        /// 返回当前对象的字符串表示，若为空则返回指定默认字符串。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="def">当对象为空时要返回的值。</param>
        /// <returns></returns>
        public static string ToStringOrDefault(this object obj, string def)
        {
            return obj == null ? def : obj.ToString();
        }
        /// <summary>
        /// 返回当前对象的字符串表示，若为空则返回指定默认字符串。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="def">当对象为空时要返回的值。</param>
        /// <returns></returns>
        public static string ToStringOrDefault(this object obj)
        {
            return ToStringOrDefault(obj, string.Empty);
        }

        /// <summary>
        /// 将对象转换为Int32类型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">转换无效时的默认值</param>
        /// <returns></returns>
        public static Int32 ToInt32(this object obj, int defValue = 0)
        {
            int result = 0;
            if (obj is int || obj is Enum)
                result = (int)obj;
            else if (obj == null)
                result = defValue;
            else
            {
                if (!int.TryParse(obj.ToString(), out result))
                {
                    result = defValue;
                }
            }
            return result;
        }

        /// <summary>
        /// 将Boolean转换为Int32类型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">转换无效时的默认值</param>
        /// <returns></returns>
        public static Int32 ToInt32(this bool b, int defValue = 0)
        {
            return b ? 1 : 0;
        }

        public static Int32 ToInt32(this decimal dec)
        {
            return decimal.ToInt32(dec);
        }

        public static Int32 ToInt32(this double d)
        {
            return (int)d;
        }

        public static Int32 ToInt32(this float f)
        {
            return (int)f;
        }

        public static Int32 ToInt32(this char c)
        {
            return (int)c - 48;
        }

        /// <summary>
        /// 将对象转换为Int64类型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">转换无效时的默认值</param>
        /// <returns></returns>
        public static Int64 ToInt64(this object obj, int defValue = 0)
        {
            long result = 0;
            if (obj is long || obj is Enum)
                result = (long)obj;
            else if (obj == null)
                result = defValue;
            else
            {
                if (!long.TryParse(obj.ToString(), out result))
                {
                    result = defValue;
                }
            }
            return result;
        }

        /// <summary>
        /// 将Boolean转换为Int64类型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">转换无效时的默认值</param>
        /// <returns></returns>
        public static Int64 ToInt64(this bool b, int defValue = 0)
        {
            return b ? 1L : 0L;
        }

        public static Int64 ToInt64(this decimal dec)
        {
            return decimal.ToInt64(dec);
        }

        public static Int64 ToInt64(this double d)
        {
            return (long)d;
        }

        public static Int64 ToInt64(this float f)
        {
            return (long)f;
        }

        public static Int64 ToInt64(this char c)
        {
            return (long)c - 48L;
        }

        /// <summary>
        /// 将对象转换为单精度浮点型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">转换无效时的默认值</param>
        /// <returns></returns>
        public static float ToFloat(this object obj, float defValue = 0f)
        {
            float result = 0f;
            if (obj is float)
                result = (float)obj;
            else if (obj == null)
                result = defValue;
            else
            {
                float.TryParse(obj.ToString(), out result);
                if (result.Equals(0f) && !defValue.Equals(0f))
                {
                    result = defValue;
                }
            }
            return result;
        }

        /// <summary>
        /// 将对象转换为Decimal类型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">转换无效时的默认值</param>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj, decimal defValue = 0m)
        {
            decimal result = 0m;
            if (obj is decimal)
                result = (decimal)obj;
            else if (obj == null)
                result = defValue;
            else
            {
                if (!decimal.TryParse(obj.ToString(), out result))
                {
                    result = defValue;
                }
            }
            return result;
        }

        /// <summary>
        /// 将对象转换为双精度浮点型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">转换无效时的默认值</param>
        /// <returns></returns>
        public static double ToDouble(this object obj, double defValue = 0d)
        {
            double result = 0d;
            if (obj is double)
                result = (double)obj;
            else if (obj == null)
                result = defValue;
            else
            {
                if (!double.TryParse(obj.ToString(), out result))
                {
                    result = defValue;
                }
            }
            return result;
        }

        /// <summary>
        /// 将对象转换为日期时间类型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj)
        {
            DateTime result = DateTime.MinValue;
            if (obj != null)
                DateTime.TryParse(obj.ToString(), out result);
            return result;
        }

        /// <summary>
        /// 将对象转换为日期时间类型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">转换无效时的默认值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj, DateTime defValue)
        {
            DateTime result = defValue;
            if (obj is DateTime)
                result = (DateTime)obj;
            else if (obj == null)
                result = defValue;
            else
            {
                if (obj != null)
                {
                    if (!DateTime.TryParse(obj.ToString(), out result))
                        result = defValue;
                }
            }
            return result;
        }

        /// <summary>
        /// 将对象转换为布尔类型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">当转换无效时的默认值</param>
        /// <returns></returns>
        public static bool ToBool(this object obj, bool defValue = false)
        {
            bool result = false;
            if (obj is bool)
                result = (bool)obj;
            else if (obj == null)
                result = defValue;
            else if (obj is int)
                result = !((int)obj).Equals(0);
            else
            {
                if (obj != null)
                {
                    var s = obj.ToString();
                    if (!(result = s.Equals("1")) && (result = !s.Equals("0")))
                    {
                        if (!bool.TryParse(s, out result))
                            result = defValue;
                    }
                }
            }
            return result;
        }

        public static T ToEnum<T>(this object obj, T defValue)
        {
            T result = defValue;
            if (obj is T)
                result = (T)obj;
            else if (obj == null)
                result = defValue;
            else if (obj is int)
                result = (T)obj;
            else
            {
                if (obj != null)
                    try
                    {
                        result = (T)Enum.Parse(typeof(T), obj.ToString());
                    }
                    catch
                    {
                        result = defValue;
                    }
            }
            return result;
        }
        #endregion

        #region 62进制编码
        internal readonly static string Chars62 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcbefghijklmnopqrstuvwxyz";

        /// <summary>
        /// 将Int32转换为62进制字符串。
        /// </summary>
        /// <param name="value">Int32实例。</param>
        /// <param name="fmtspace">补位格式化字符串。（例：“00000000”）</param>
        /// <returns>返回62进制字符串。</returns>
        public static string ToBase62(this Int32 value, string fmtspace)
        {
            StringBuilder result = new StringBuilder(fmtspace);
            var flen = fmtspace.Length - 1;
            while (value > 0)
            {
                var c = Chars62[value % 62];
                value /= 62;
                if (flen >= 0)
                {
                    result.Replace(result[flen], c, flen, 1);
                    --flen;
                }
                else
                    result.Insert(0, c);
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将Int32转换为62进制字符串。
        /// </summary>
        /// <param name="value">Int32实例。</param>
        /// <returns>返回62进制字符串。</returns>
        public static string ToBase62(this Int32 value)
        {
            StringBuilder result = new StringBuilder(8);
            while (value > 0)
            {
                result.Insert(0, Chars62[value % 62]);
                value /= 62;
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将UInt32转换为62进制字符串。
        /// </summary>
        /// <param name="value">UInt32实例。</param>
        /// <param name="fmtspace">补位格式化字符串。（例：“00000000”）</param>
        /// <returns>返回62进制字符串。</returns>
        public static string ToBase62(this UInt32 value, string fmtspace)
        {
            StringBuilder result = new StringBuilder(fmtspace);
            var flen = fmtspace.Length - 1;
            while (value > 0)
            {
                var c = Chars62[(int)(value % 62)];
                value /= 62;
                if (flen >= 0)
                {
                    result.Replace(result[flen], c, flen, 1);
                    --flen;
                }
                else
                    result.Insert(0, c);
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将UInt32转换为62进制字符串。
        /// </summary>
        /// <param name="value">UInt32实例。</param>
        /// <returns>返回62进制字符串。</returns>
        public static string ToBase62(this UInt32 value)
        {
            StringBuilder result = new StringBuilder(8);
            while (value > 0)
            {
                result.Insert(0, Chars62[(int)(value % 62)]);
                value /= 62;
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将Int64转换为62进制字符串。
        /// </summary>
        /// <param name="value">Int64实例。</param>
        /// <param name="fmtspace">补位格式化字符串。（例：“00000000”）</param>
        /// <returns>返回62进制字符串。</returns>
        public static string ToBase62(this Int64 value, string fmtspace)
        {
            StringBuilder result = new StringBuilder(fmtspace);
            var flen = fmtspace.Length - 1;
            while (value > 0)
            {
                var c = Chars62[(int)(value % 62)];
                value /= 62;
                if (flen >= 0)
                {
                    result.Replace(result[flen], c, flen, 1);
                    --flen;
                }
                else
                    result.Insert(0, c);
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将Int64转换为62进制字符串。
        /// </summary>
        /// <param name="value">Int64实例。</param>
        /// <returns>返回62进制字符串。</returns>
        public static string ToBase62(this Int64 value)
        {
            StringBuilder result = new StringBuilder(8);
            while (value > 0)
            {
                result.Insert(0, Chars62[(int)(value % 62)]);
                value /= 62;
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将UInt64转换为62进制字符串。
        /// </summary>
        /// <param name="value">UInt64实例。</param>
        /// <param name="fmtspace">补位格式化字符串。（例：“00000000”）</param>
        /// <returns>返回62进制字符串。</returns>
        public static string ToBase62(this UInt64 value, string fmtspace)
        {
            StringBuilder result = new StringBuilder(fmtspace);
            var flen = fmtspace.Length - 1;
            while (value > 0)
            {
                var c = Chars62[(int)(value % 62)];
                value /= 62;
                if (flen >= 0)
                {
                    result.Replace(result[flen], c, flen, 1);
                    --flen;
                }
                else
                    result.Insert(0, c);
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将UInt64转换为62进制字符串。
        /// </summary>
        /// <param name="value">UInt64实例。</param>
        /// <returns>返回62进制字符串。</returns>
        public static string ToBase62(this UInt64 value)
        {
            StringBuilder result = new StringBuilder(8);
            while (value > 0)
            {
                result.Insert(0, Chars62[(int)(value % 62)]);
                value /= 62;
            }
            return result.Length < 1 ? "0" : result.ToString();
        }
        #endregion

        #region 62解码
        /// <summary>
        /// 将62进制字符串转换为Int32类型。
        /// </summary>
        /// <param name="s">62进制字符串。</param>
        /// <returns>返回Int32值类型。</returns>
        public static Int32 Int32FromBase62(this string s)
        {
            int result = 0, len = s.Length;
            for (int i = 0; i < len;)
            {
                var sx = Chars62.IndexOf(s[i]);
                if (sx < 0)
                    continue;
                result += sx * (int)Math.Pow(62, len - ++i);
            }
            return result;
        }

        /// <summary>
        /// 将62进制字符串转换为UInt32类型。
        /// </summary>
        /// <param name="s">62进制字符串。</param>
        /// <returns>返回UInt32值类型。</returns>
        public static UInt32 UInt32FromBase62(this string s)
        {
            UInt32 result = 0;
            int len = s.Length;
            for (int i = 0; i < len;)
            {
                var sx = (UInt32)Chars62.IndexOf(s[i]);
                if (sx < 0)
                    continue;
                result += sx * (UInt32)Math.Pow(62, len - ++i);
            }
            return result;
        }

        /// <summary>
        /// 将62进制字符串转换为Int64类型。
        /// </summary>
        /// <param name="s">62进制字符串。</param>
        /// <returns>返回Int64值类型。</returns>
        public static Int64 Int64FromBase62(this string s)
        {
            Int64 result = 0;
            int len = s.Length;
            for (int i = 0; i < len;)
            {
                var sx = (Int64)Chars62.IndexOf(s[i]);
                if (sx < 0)
                    continue;
                result += sx * (Int64)Math.Pow(62, len - ++i);
            }
            return result;
        }

        /// <summary>
        /// 将62进制字符串转换为UInt64类型。
        /// </summary>
        /// <param name="s">62进制字符串。</param>
        /// <returns>返回UInt64值类型。</returns>
        public static UInt64 UInt64FromBase62(this string s)
        {
            UInt64 result = 0;
            int len = s.Length;
            for (int i = 0; i < len;)
            {
                var sx = (UInt64)Chars62.IndexOf(s[i]);
                if (sx < 0)
                    continue;
                result += sx * (UInt64)Math.Pow(62, len - ++i);
            }
            return result;
        }
        #endregion

        #region 36进制编码
        internal readonly static string Chars36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// 将Int16转换为36进制字符串。
        /// </summary>
        /// <param name="value">Int16实例。</param>
        /// <param name="fmtspace">补位格式化字符串。（例：“00000000”）</param>
        /// <returns>返回36进制字符串。</returns>
        public static string ToBase36(this Int16 value, string fmtspace)
        {
            StringBuilder result = new StringBuilder(fmtspace);
            var flen = fmtspace.Length - 1;
            while (value > 0)
            {
                var c = Chars36[value % 36];
                value /= 36;
                if (flen >= 0)
                {
                    result.Replace(result[flen], c, flen, 1);
                    --flen;
                }
                else
                    result.Insert(0, c);
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将Int16转换为36进制字符串。
        /// </summary>
        /// <param name="value">Int16实例。</param>
        /// <returns>返回36进制字符串。</returns>
        public static string ToBase36(this Int16 value)
        {
            StringBuilder result = new StringBuilder(8);
            while (value > 0)
            {
                result.Insert(0, Chars36[value % 36]);
                value /= 36;
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将UInt16转换为36进制字符串。
        /// </summary>
        /// <param name="value">UInt16实例。</param>
        /// <param name="fmtspace">补位格式化字符串。（例：“00000000”）</param>
        /// <returns>返回36进制字符串。</returns>
        public static string ToBase36(this UInt16 value, string fmtspace)
        {
            StringBuilder result = new StringBuilder(fmtspace);
            var flen = fmtspace.Length - 1;
            while (value > 0)
            {
                var c = Chars36[(int)(value % 36)];
                value /= 36;
                if (flen >= 0)
                {
                    result.Replace(result[flen], c, flen, 1);
                    --flen;
                }
                else
                    result.Insert(0, c);
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将UInt16转换为36进制字符串。
        /// </summary>
        /// <param name="value">UInt32实例。</param>
        /// <returns>返回36进制字符串。</returns>
        public static string ToBase36(this UInt16 value)
        {
            StringBuilder result = new StringBuilder(8);
            while (value > 0)
            {
                result.Insert(0, Chars36[(int)(value % 36)]);
                value /= 36;
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将Int32转换为36进制字符串。
        /// </summary>
        /// <param name="value">Int32实例。</param>
        /// <param name="fmtspace">补位格式化字符串。（例：“00000000”）</param>
        /// <returns>返回36进制字符串。</returns>
        public static string ToBase36(this Int32 value, string fmtspace)
        {
            StringBuilder result = new StringBuilder(fmtspace);
            var flen = fmtspace.Length - 1;
            while (value > 0)
            {
                var c = Chars36[value % 36];
                value /= 36;
                if (flen >= 0)
                {
                    result.Replace(result[flen], c, flen, 1);
                    --flen;
                }
                else
                    result.Insert(0, c);
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将Int32转换为36进制字符串。
        /// </summary>
        /// <param name="value">Int32实例。</param>
        /// <returns>返回36进制字符串。</returns>
        public static string ToBase36(this Int32 value)
        {
            StringBuilder result = new StringBuilder(8);
            while (value > 0)
            {
                result.Insert(0, Chars36[value % 36]);
                value /= 36;
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将UInt32转换为36进制字符串。
        /// </summary>
        /// <param name="value">UInt32实例。</param>
        /// <param name="fmtspace">补位格式化字符串。（例：“00000000”）</param>
        /// <returns>返回36进制字符串。</returns>
        public static string ToBase36(this UInt32 value, string fmtspace)
        {
            StringBuilder result = new StringBuilder(fmtspace);
            var flen = fmtspace.Length - 1;
            while (value > 0)
            {
                var c = Chars36[(int)(value % 36)];
                value /= 36;
                if (flen >= 0)
                {
                    result.Replace(result[flen], c, flen, 1);
                    --flen;
                }
                else
                    result.Insert(0, c);
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将UInt32转换为36进制字符串。
        /// </summary>
        /// <param name="value">UInt32实例。</param>
        /// <returns>返回36进制字符串。</returns>
        public static string ToBase36(this UInt32 value)
        {
            StringBuilder result = new StringBuilder(8);
            while (value > 0)
            {
                result.Insert(0, Chars36[(int)(value % 36)]);
                value /= 36;
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将Int64转换为36进制字符串。
        /// </summary>
        /// <param name="value">Int64实例。</param>
        /// <param name="fmtspace">补位格式化字符串。（例：“00000000”）</param>
        /// <returns>返回36进制字符串。</returns>
        public static string ToBase36(this Int64 value, string fmtspace)
        {
            StringBuilder result = new StringBuilder(fmtspace);
            var flen = fmtspace.Length - 1;
            while (value > 0)
            {
                var c = Chars36[(int)(value % 36)];
                value /= 36;
                if (flen >= 0)
                {
                    result.Replace(result[flen], c, flen, 1);
                    --flen;
                }
                else
                    result.Insert(0, c);
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将Int64转换为36进制字符串。
        /// </summary>
        /// <param name="value">Int64实例。</param>
        /// <returns>返回36进制字符串。</returns>
        public static string ToBase36(this Int64 value)
        {
            StringBuilder result = new StringBuilder(8);
            while (value > 0)
            {
                result.Insert(0, Chars36[(int)(value % 36)]);
                value /= 36;
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将UInt64转换为36进制字符串。
        /// </summary>
        /// <param name="value">UInt64实例。</param>
        /// <param name="fmtspace">补位格式化字符串。（例：“00000000”）</param>
        /// <returns>返回36进制字符串。</returns>
        public static string ToBase36(this UInt64 value, string fmtspace)
        {
            StringBuilder result = new StringBuilder(fmtspace);
            var flen = fmtspace.Length - 1;
            while (value > 0)
            {
                var c = Chars36[(int)(value % 36)];
                value /= 36;
                if (flen >= 0)
                {
                    result.Replace(result[flen], c, flen, 1);
                    --flen;
                }
                else
                    result.Insert(0, c);
            }
            return result.Length < 1 ? "0" : result.ToString();
        }

        /// <summary>
        /// 将UInt64转换为36进制字符串。
        /// </summary>
        /// <param name="value">UInt64实例。</param>
        /// <returns>返回36进制字符串。</returns>
        public static string ToBase36(this UInt64 value)
        {
            StringBuilder result = new StringBuilder(8);
            while (value > 0)
            {
                result.Insert(0, Chars36[(int)(value % 36)]);
                value /= 36;
            }
            return result.Length < 1 ? "0" : result.ToString();
        }
        #endregion

        #region 36解码
        /// <summary>
        /// 将36进制字符串转换为Int16类型。
        /// </summary>
        /// <param name="s">36进制字符串。</param>
        /// <returns>返回Int16值类型。</returns>
        public static Int16 Int16FromBase36(this string s)
        {
            short result = 0;
            int len = s.Length;
            for (int i = 0; i < len;)
            {
                var sx = Chars36.IndexOf(s[i]);
                if (sx < 0)
                    continue;
                result += (short)(sx * (int)Math.Pow(36, len - ++i));
            }
            return result;
        }

        /// <summary>
        /// 将36进制字符串转换为UInt16类型。
        /// </summary>
        /// <param name="s">36进制字符串。</param>FromBase36
        /// <returns>返回UInt16值类型。</returns>
        public static UInt16 UInt16FromBase36(this string s)
        {
            UInt16 result = 0;
            int len = s.Length;
            for (int i = 0; i < len;)
            {
                var sx = (UInt32)Chars36.IndexOf(s[i]);
                if (sx < 0)
                    continue;
                result += (UInt16)(sx * (UInt32)Math.Pow(36, len - ++i));
            }
            return result;
        }

        /// <summary>
        /// 将36进制字符串转换为Int32类型。
        /// </summary>
        /// <param name="s">36进制字符串。</param>
        /// <returns>返回Int32值类型。</returns>
        public static Int32 Int32FromBase36(this string s)
        {
            int result = 0, len = s.Length;
            for (int i = 0; i < len;)
            {
                var sx = Chars36.IndexOf(s[i]);
                if (sx < 0)
                    continue;
                result += sx * (int)Math.Pow(36, len - ++i);
            }
            return result;
        }

        /// <summary>
        /// 将36进制字符串转换为UInt32类型。
        /// </summary>
        /// <param name="s">36进制字符串。</param>FromBase36
        /// <returns>返回UInt32值类型。</returns>
        public static UInt32 UInt32FromBase36(this string s)
        {
            UInt32 result = 0;
            int len = s.Length;
            for (int i = 0; i < len;)
            {
                var sx = (UInt32)Chars36.IndexOf(s[i]);
                if (sx < 0)
                    continue;
                result += sx * (UInt32)Math.Pow(36, len - ++i);
            }
            return result;
        }

        /// <summary>
        /// 将36进制字符串转换为Int64类型。
        /// </summary>
        /// <param name="s">36进制字符串。</param>
        /// <returns>返回Int64值类型。</returns>
        public static Int64 Int64FromBase36(this string s)
        {
            Int64 result = 0;
            int len = s.Length;
            for (int i = 0; i < len;)
            {
                var sx = (Int64)Chars36.IndexOf(s[i]);
                if (sx < 0)
                    continue;
                result += sx * (Int64)Math.Pow(36, len - ++i);
            }
            return result;
        }

        /// <summary>
        /// 将36进制字符串转换为UInt64类型。
        /// </summary>
        /// <param name="s">36进制字符串。</param>
        /// <returns>返回UInt64值类型。</returns>
        public static UInt64 UInt64FromBase36(this string s)
        {
            UInt64 result = 0;
            int len = s.Length;
            for (int i = 0; i < len;)
            {
                var sx = (UInt64)Chars36.IndexOf(s[i]);
                if (sx < 0)
                    continue;
                result += sx * (UInt64)Math.Pow(36, len - ++i);
            }
            return result;
        }
        #endregion

        #region XElement Expasion
        public static string Attr(this System.Xml.Linq.XElement xe, System.Xml.Linq.XName xname)
        {
            var attr = xe.Attribute(xname);
            if (attr == null)
                return string.Empty;
            else
                return attr.Value;
        }

        public static string Attr(this System.Xml.Linq.XElement xe, System.Xml.Linq.XName xname, object defValue)
        {
            var attr = xe.Attribute(xname);
            if (attr == null)
                return defValue.ToStringOrDefault();
            else
                return attr.Value;
        }

        public static string Val(this System.Xml.Linq.XElement xe)
        {
            var val = xe;
            if (val != null)
                return val.Value ?? string.Empty;
            else
                return string.Empty;
        }

        public static string Val(this System.Xml.Linq.XElement xe, System.Xml.Linq.XName xname)
        {
            var val = xe;
            if (val != null && !xname.LocalName.IsNullOrEmpty())
                val = xe.Element(xname);
            if (val != null)
                return val.Value;
            else
                return string.Empty;
        }

        public static string Val(this System.Xml.Linq.XElement xe, System.Xml.Linq.XName xname, object defValue)
        {
            var val = xe;
            if (val != null && !xname.LocalName.IsNullOrEmpty())
                val = xe.Element(xname);
            if (val != null)
                return val.Value;
            else
                return defValue.ToStringOrDefault();
        }
        #endregion

        #region Join String
        public static string JoinString(this string[] strs, string spliter = "")
        {
            return string.Join(spliter, strs);
        }
        public static string JoinString(this IEnumerable<string> strs, string spliter = "")
        {
            return string.Join(spliter, strs.ToArray());
        }
        #endregion

        #region 日期时间扩展
        public static int GetCNDayOfWeek(this DateTime dt)
        {
            var res = dt.DayOfWeek.ToInt32();
            if (res.Equals(0))
                res = 7;
            return res;
        }
        #endregion

        #region Int32 To Chinese
        static char[] s_cnNums = new char[] { '零', '一', '二', '三', '四', '五', '六', '七', '八', '九' };
        static char[] s_cnUnit = new char[] { '十', '百', '千', '万', '亿' };
        /// <summary>
        /// 将Int32数字转换为中文大写字符串。
        /// </summary>
        /// <param name="num">要转换的数字</param>
        /// <returns></returns>
        public static string ToChinese(this int num)
        {
            var nStr = num.ToString();
            string result = string.Empty;
            var sb = new StringBuilder((nStr.Length * 2) + (nStr.Length / 3));
            if (num < 0)
            {
                sb.Append("负");
                nStr = nStr.Remove(0, 1);
            }
            var len = nStr.Length;
            var pZero = false;
            for (int i = 0; i < len; ++i)
            {
                var c = s_cnNums[nStr[i].ToInt32()];

                var pos = len - i;
                if (!pZero && c.Equals(s_cnNums[0]))
                {
                    sb.Append(c);
                    pZero = true;
                }
                else if (!c.Equals(s_cnNums[0]))
                {
                    pos = pos - 1;
                    sb.Append(c);
                    pZero = false;
                    var idx = (pos & 3) - 1;
                    if (idx >= 0)
                        sb.Append(s_cnUnit[idx]);
                    else
                    {
                        idx = (pos & 4) - 1;
                        if (idx >= 0)
                            sb.Append(s_cnUnit[idx]);
                        else
                        {
                            idx = (pos & 8) - 4;
                            if (idx >= 0)
                                sb.Append(s_cnUnit[idx]);
                        }
                    }
                }
                else if (pZero && ((pos & 4) != 0 || (pos & 8) != 0) && (pos & 3) == 0)
                {
                    var idx = (pos & 4) - 1;
                    if (idx >= 0)
                    {
                        if (!sb[sb.Length - 2].Equals(s_cnUnit[4]))
                            sb.Insert(sb.Length - 1, s_cnUnit[idx]);
                    }
                    else
                    {
                        idx = (pos & 8) - 4;
                        if (idx >= 0)
                            sb.Append(s_cnUnit[idx]);
                    }
                }
            }
            if (sb[sb.Length - 1].Equals(s_cnNums[0]))
                sb.Remove(sb.Length - 1, 1);
            result = sb.ToString();
            sb.Remove(0, sb.Length);
            sb = null;
            return result;
        }
        #endregion

        #region 数组扩展
        /// 合并数组
        /// </summary>
        /// <param name="source">第一个数组</param>
        /// <param name="Second">第二个数组</param>
        /// <returns>合并后的数组(第一个数组+第二个数组，长度为两个数组的长度)</returns>
        public static string[] MergeArray(this string[] first, string[] second)
        {
            string[] result = new string[first.Length + second.Length];
            first.CopyTo(result, 0);
            second.CopyTo(result, first.Length);
            return result;
        }
        #endregion
    }
}
