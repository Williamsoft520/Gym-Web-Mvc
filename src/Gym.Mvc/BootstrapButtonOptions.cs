namespace System.Web.Mvc
{
    #region BootstrapButtonOptions
    /// <summary>
    /// 表示带有 Bootstrap 风格的按钮设置。
    /// </summary>
    public class BootstrapButtonOptions
    {
        /// <summary>
        /// 获取或设置按钮的文本，支持 Html 字符串。
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 获取或设置按钮的类型。
        /// </summary>
        public ButtonTypes Type { get; set; } = ButtonTypes.Button;
        /// <summary>
        /// 获取或设置按钮的颜色。
        /// </summary>
        public ColorTypes Color { get; set; } = ColorTypes.Primary;
        /// <summary>
        /// 获取或设置按钮的大小。
        /// </summary>
        public SizeTypes Size { get; set; } = SizeTypes.Normal;

        /// <summary>
        /// 获取或设置图标的样式名称。参照 FontAwesome.io 的风格。
        /// </summary>
        public string IconClass { get; set; }
        /// <summary>
        /// 获取或设置图标相对于按钮文本的位置，仅支持 Left 和 Right 两种。
        /// </summary>
        public DirectionLayout IconDirection { get; set; } = DirectionLayout.Left;

        /// <summary>
        /// 获取或设置其他的样式名称。将会放在 Bootstrap 样式后面。
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// 获取或设置超链接地址。
        /// </summary>
        public string Link { get; set; }
    }
    #endregion
}
