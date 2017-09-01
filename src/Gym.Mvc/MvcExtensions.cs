using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace System.Web.Mvc
{
    /// <summary>
    /// 表示在应用程序中支持 HTML。
    /// </summary>
    public static class MvcExtensions
    {
        /// <summary>
        /// 将 &lt;form> 开始标记写入响应，并将操作标记设置为指定的操作。表单使用指定的 HTTP POST 方法，并包含 HTML 特性。
        /// </summary>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="actionName">操作方法的名称。</param>
        /// <param name="controllerName">控制器的名称。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <param name="routeValues">一个包含路由参数的对象。通过检查对象的属性，利用反射检索参数。此对象通常是使用对象初始值设定项语法创建的。</param>
        /// <returns>由一个 form 组成的表单区域。</returns>
        public static MvcForm BeginPostForm(this HtmlHelper htmlHelper, string actionName, string controllerName=null, object htmlAttributes = null, object routeValues = null)
        {
            return htmlHelper.BeginForm(actionName, controllerName, routeValues, FormMethod.Post, htmlAttributes);
        }

        /// <summary>
        /// 生成分页链接Url
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="page">页码</param>
        /// <param name="options">配置</param>
        /// <returns></returns>
        internal static string GeneratePaginationUrl(this HtmlHelper htmlHelper, long page, BootstrapPagedButtonOptions options)
        {
            if (!htmlHelper.ViewContext.IsChildAction && htmlHelper.ViewContext.RouteData.Values.TryGetValue("page", out object pageIndexObj))
            {
                htmlHelper.ViewContext.RouteData.Values["page"] = page;

                return UrlHelper.GenerateUrl(options.RouteName, options.ActionName, options.ControllerName, htmlHelper.ViewContext.RouteData.Values, RouteTable.Routes, htmlHelper.ViewContext.RequestContext, false);
            }

            var currentUrl = htmlHelper.ViewContext.RequestContext.HttpContext.Server.HtmlEncode(htmlHelper.ViewContext.HttpContext.Request.RawUrl);

            if (currentUrl.IndexOf("?") == -1)
            {
                return string.Format("{0}?page={1}",currentUrl, page);
            }

            if (currentUrl.IndexOf("page=", StringComparison.InvariantCultureIgnoreCase) == -1)
            {
                return string.Format("{0}&page={1}",currentUrl, page);
            }
            return Regex.Replace(currentUrl,@"page=(\d+\.?\d*|\.\d+)", $"page={page}", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        internal static void GeneratePageButton(this HtmlHelper htmlHelper,
    TagBuilder pageBuilder,
    bool canClick, long page, string text, BootstrapPagedButtonOptions options,bool current=false)
        {
            TagBuilder liTag = new TagBuilder("li");

            if (canClick)
            {
                if (current)
                {
                    liTag.AddCssClass("active");
                }
                else
                {
                    liTag.AddCssClass("disabled");
                }
                liTag.InnerHtml += $"<a href='javascript:;'>{text}</a>";
            }
            else
            {
                liTag.InnerHtml += string.Format("<a href='{0}'>{1}</a>",htmlHelper.GeneratePaginationUrl(page, options), text);
            }

            pageBuilder.InnerHtml += liTag.ToString();
        }

        /// <summary>
        /// 将指定枚举对象构建为下拉菜单所需的 SelectListItem 集合对象
        /// </summary>
        /// <param name="enumType">枚举的类型。</param>
        /// <param name="selectedValue">被选中的值。</param>
        /// <returns>下拉框所需的 SelectListItem 集合</returns>
        public static IEnumerable<SelectListItem> EnumToDropDownList(this Type enumType, object selectedValue)

            => enumType.GetEnumItems()
                .Select((k, v) => new SelectListItem
                {
                    Text = k.Key,
                    Value = k.Value.ToString(),
                    Selected = k.Key == selectedValue?.ToString()
                });

        /// <summary>
        /// 表示输出一个 img 元素。
        /// </summary>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="src">图片的 src 链接地址。</param>
        /// <param name="text">设置 img 元素中的 alt 和 title 属性，帮助加强 html 规范。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个 HTML img 元素。</returns>
        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string src, string text = null, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.ObjectToDictionary(htmlAttributes);

            TagBuilder tag = new TagBuilder("img");
            tag.Attributes.Add("src", src);
            tag.Attributes.Add("title", text);
            tag.Attributes.Add("alt", text);
            tag.MergeAttributes(attributes);

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.SelfClosing));
        }

    }

}
