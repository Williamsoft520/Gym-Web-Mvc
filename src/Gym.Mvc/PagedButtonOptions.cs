using System.Text.RegularExpressions;
using System.Web.Routing;

namespace System.Web.Mvc
{
    /// <summary>
    /// 支持 Bootstrap 的分页按钮控件配置。
    /// </summary>
    public class BootstrapPagedButtonOptions
    {
        /// <summary>
        /// 初始化 <see cref="BootstrapPagedButtonOptions"/> 类的新实例。
        /// </summary>
        public BootstrapPagedButtonOptions()
        {
            //this.NumbericCount = 10;
            // this.AlwaysShow = true;
            //this.ButtonMode = PageButtonMode.PreviousNextFirstLastNumberic;
            ////this.ContainerTag = "ul";
        }
        /// <summary>
        /// 获取或设置分页显示的数字的数目。若设置为3，表示在中间的数字数目只显示近3页的数字；例如这样的形式：首页 上一页 2 3 4 下一页 末页。
        /// </summary>
        public int NumbericCount { get; set; } = 10;
        /// <summary>
        /// 获取或设置分页按钮是否一直显示。若为 true，当第一页的数据没有达到所需要分页的数据量时，分页组件将一直显示。否则，直到数据可以分页时才显示。
        /// </summary>
        public bool AlwaysShow { get; set; } = true;
        /// <summary>
        /// 获取或设置分页按钮显示的模式。
        /// </summary>
        public PageButtonMode ButtonMode { get; set; } = PageButtonMode.PreviousNextNumberic;

        /// <summary>
        /// 获取或设置分页布局的尺寸。仅支持 SM 和 LG。
        /// </summary>
        public SizeTypes Size { get; set; } = SizeTypes.Normal;

        /// <summary>
        /// 获取或设置“首页”的文本，支持 HTML 文本。
        /// </summary>
        public string TextOfFirst { get; set; } = "首页";

        /// <summary>
        /// 获取或设置“上一页”的文本，支持 HTML 文本。
        /// </summary>
        public string TextOfPrevious { get; set; } = "上一页";

        /// <summary>
        /// 获取或设置“下一页”的文本，支持 HTML 文本。
        /// </summary>
        public string TextOfNext { get; set; } = "下一页";

        /// <summary>
        /// 获取或设置“末页”的文本，支持 HTML 文本。
        /// </summary>
        public string TextOfLast { get; set; } = "末页";

        /// <summary>
        /// 获取或设置一个对象，其中包含要为该元素设置的 HTML 特性。
        /// </summary>
        public object HtmlAttributes { get; set; }

        /// <summary>
        /// 获取或设置路由名称。
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 获取或设置控制器名称。
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 获取或设置行为名称。
        /// </summary>
        public string ActionName { get; set; }
    }

    /// <summary>
    /// 分页按钮模式
    /// </summary>
    public enum PageButtonMode
    {
        /// <summary>
        /// 上一页，下一页
        /// </summary>
        PreviousNext,
        /// <summary>
        /// 上一页，下一页，分页数字
        /// </summary>
        PreviousNextNumberic,
        /// <summary>
        /// 上一页，下一页，首页，末页
        /// </summary>
        PreviousNextFirstLast,
        /// <summary>
        /// 首页，末页，分页数字
        /// </summary>
        FirstLastNumberic,
        /// <summary>
        /// 上一页，下一页，首页，末页，分页数字
        /// </summary>
        PreviousNextFirstLastNumberic,
        /// <summary>
        /// 只显示数字
        /// </summary>
        OnlyNumberic,
    }
}
