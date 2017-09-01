namespace System.Web.Mvc
{
    #region ButtonTypes
    /// <summary>
    /// 表示按钮类型
    /// </summary>
    public enum ButtonTypes
    {
        /// <summary>
        /// 普通按钮，不带任何效果
        /// </summary>
        Button = 1,
        /// <summary>
        /// 提交按钮，将触发所在表单
        /// </summary>
        Submit = 2,
        /// <summary>
        /// 重置按钮，将清空文本框和其他控件到初始化值
        /// </summary>
        Reset = 3,
        /// <summary>
        /// 链接按钮，将按钮变为一个链接样式，类似于 <c>&lt;a></c>标记
        /// </summary>
        Link = 4
    }
    #endregion

    #region ColorTypes
    /// <summary>
    /// 前端框架的颜色类型，根据样式有所不同。
    /// </summary>
    public enum ColorTypes
    {
        /// <summary>
        /// 默认颜色。
        /// </summary>
        Default = 0,
        /// <summary>
        /// 主色调。
        /// </summary>
        Primary = 1,
        /// <summary>
        /// 作为普通显示信息颜色。
        /// </summary>
        Info = 2,
        /// <summary>
        /// 作为成功的颜色。
        /// </summary>
        Success = 3,
        /// <summary>
        /// 作为警告的颜色。
        /// </summary>
        Warning = 4,
        /// <summary>
        /// 作为危险且醒目的颜色。
        /// </summary>
        Danger = 5
    }
    #endregion

    #region SizeTypes
    /// <summary>
    /// 控件尺寸类型
    /// </summary>
    public enum SizeTypes
    {
        /// <summary>
        /// 很小，xs
        /// </summary>
        XS,
        /// <summary>
        /// 小，sm
        /// </summary>
        SM,
        /// <summary>
        /// 正常，默认
        /// </summary>
        Normal,
        /// <summary>
        /// 大，lg
        /// </summary>
        LG,
        /// <summary>
        /// 很大，xl
        /// </summary>
        XL
    }
    #endregion

    #region DirectionLayout
    /// <summary>
    /// 控件所在布局位置，具体支持需要查看控件本身
    /// </summary>
    public enum DirectionLayout
    {
        /// <summary>
        /// 左方
        /// </summary>
        Left = 1,
        /// <summary>
        /// 右方
        /// </summary>
        Right = 2,
        /// <summary>
        /// 上方
        /// </summary>
        Top = 3,
        /// <summary>
        /// 下方
        /// </summary>
        Bottom = 4
    }
    #endregion

    #region ImageType
    /// <summary>
    /// 图像的显示类型。
    /// </summary>
    public enum ImageTypes
    {
        /// <summary>
        /// 不使用任何样式。
        /// </summary>
        None,
        /// <summary>
        /// img-rounded 样式，图像四个角是圆滑形状。
        /// </summary>
        Rounded,
        /// <summary>
        /// img-circle 样式，图像呈现圆形。
        /// </summary>
        Circle,
        /// <summary>
        /// img-thumbnail 样式，图像加边框，类似相册封面的样子。
        /// </summary>
        Thumbnail,
    }
    #endregion
}
