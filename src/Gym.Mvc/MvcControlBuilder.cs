using System.Collections.Generic;

namespace System.Web.Mvc
{
    /// <summary>
    /// 常用的Bootstrap Mvc建造类，提供快速输出一些简单内容。
    /// </summary>
    internal static class MvcControlBuilder
    {
        /// <summary>
        /// 根据ColorType生成对应的Bootstrap所适应的颜色class
        /// </summary>
        /// <param name="attributes">该元素设置的 HTML 特性。</param>
        /// <param name="prefix">前缀，例如按钮是btn。</param>
        /// <param name="color">颜色</param>
        public static void BuildColorClass(this IDictionary<string, object> attributes, string prefix=null,ColorTypes color= ColorTypes.Primary)
        {
            attributes.AddOrMerge("class", " {0}{1} ".StringFormat(prefix,color.ToString().ToLower()));
        }

        /// <summary>
        /// 根据SizeTypes生成对应的Bootstrap所适应的尺寸class
        /// </summary>
        /// <param name="attributes">该元素设置的 HTML 特性。</param>
        /// <param name="prefix">前缀，例如按钮是btn。</param>
        /// <param name="size">尺寸</param>
        public static void BuildSizeClass(this IDictionary<string, object> attributes, string prefix=null,SizeTypes size= SizeTypes.Normal)
        {
            if(size== SizeTypes.Normal)
            {
                return;
            }
            attributes.AddOrMerge("class", " {0}{1} ".StringFormat(prefix, size.ToString().ToLower()));
        }

        ///// <summary>
        ///// 生成指定名称的Awesome图标
        ///// </summary>
        ///// <param name="icon">图标名称。具体请参照  http://fortawesome.io/Font-Awesome/examples/"命名。PS：只需要写“fa-”后面部分的字符串即可。例如：示例为<code>“fa fa-ok”</code>，该参数仅需要写<code>“ok”</code>即可。当该值不为null或空字符串时有效。</param>
        ///// <returns>输出HTML i 元素，并将class设置为fa fa-图标</returns>
        //public static MvcHtmlString GenerateAwesomeIcon(string icon)
        //{
        //    //创建icon
        //    if (!icon.IsNullOrEmpty())
        //    {
        //        TagBuilder iconBuilder = new TagBuilder("i");
        //        iconBuilder.MergeAttribute("class", "fa fa-{0}".StringFormat(icon));
        //        return MvcHtmlString.Create(iconBuilder.ToString());
        //    }
        //    return MvcHtmlString.Empty;
        //}
    }

}
