using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace LongQin.Infrastructures
{
    public static class HtmlHelperExtend
    {
        public static MvcHtmlString Pager(this HtmlHelper html, int totalPage, int curPageIndex, string actionName, object routeValues)
        {
            var routeValuesDynamic = new RouteValueDictionary(routeValues);
            var sb = new StringBuilder();
            routeValuesDynamic["page"] = 1;
            sb.AppendFormat("<li>{0}</li>", html.ActionLink("首页", actionName, routeValuesDynamic));
            if (curPageIndex == 1)
            {
                sb.Append("<li class=\"disabled\"><a href=\"javascript:void(0)\">上一页</a></li>");
            }
            else
            {
                routeValuesDynamic["page"] = curPageIndex - 1;
                sb.AppendFormat("<li>{0}</li>", html.ActionLink("上一页", actionName, routeValuesDynamic));
            }

            var minIndex = Math.Max(curPageIndex - 2, 1);
            var maxIndex = Math.Min(curPageIndex + 2, totalPage);
            for (var i = minIndex; i <= maxIndex; i++)
            {
                if (i == curPageIndex)
                {
                    sb.AppendFormat(
                        "<li class=\"active\"><a href=\"javascript:void(0)\">{0}</a></li>",
                        curPageIndex);
                }
                else
                {
                    routeValuesDynamic["page"] = i;
                    sb.AppendFormat("<li>{0}</li>", html.ActionLink(i.ToString(), actionName, routeValuesDynamic));
                }
            }

            if (curPageIndex == totalPage)
            {
                sb.Append("<li class=\"disabled\"><a href=\"javascript:void(0)\">下一页</a></li>");
            }
            else
            {
                routeValuesDynamic["page"] = curPageIndex + 1;
                sb.AppendFormat("<li>{0}</li>", html.ActionLink("下一页", actionName, routeValuesDynamic));
            }
            routeValuesDynamic["page"] = totalPage;
            sb.AppendFormat("<li>{0}</li>", html.ActionLink("末页", actionName, routeValuesDynamic));

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}