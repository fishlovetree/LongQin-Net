using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Repository
{
    public static class Extensions
    {
        /// <summary>
        /// 获取对象属性列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">对象</param>
        /// <param name="removeFields">需要排除的属性列表</param>
        /// <returns></returns>
        public static List<string> ToFields<T>(this T model, List<string> removeFields = null)
        {
            if(model == null)
            {
                return null;
            }

            var properties = model.GetType().GetProperties().Select(n => n.Name).ToList();
            if (removeFields != null)
            {
                properties.RemoveWhere(n => removeFields.Contains(n, new FieldEqualityComparer()));
            }

            return properties;
        }
    }
}
