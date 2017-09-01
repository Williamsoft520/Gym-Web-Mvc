using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc.Html;

namespace System.Web.Mvc
{
    /// <summary>
    /// 表示在应用程序中支持 HTML 输入控件。
    /// </summary>
    public static class BootstrapExtensions
    {
        /// <summary>
        /// 利用DisplayAttribute特性，创建文本框中的PlaceHolder属性
        /// </summary>
        /// <typeparam name="TModel">模型的类型</typeparam>
        /// <typeparam name="TProperty">值的类型</typeparam>
        /// <param name="expression"> 一个表达式，用于标识包含要呈现的属性的对象。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        public static void BuildPlaceHolder<TModel, TProperty>(this IDictionary<string, object> htmlAttributes, Expression<Func<TModel, TProperty>> expression)
        {
            if (expression.Body.NodeType != ExpressionType.MemberAccess)
            {
                return;
            }

            MemberExpression memberExpression = (MemberExpression)expression.Body;
            if (!(memberExpression.Member is PropertyInfo))
            {
                return;
            }

            Type model = typeof(TModel);
            string propertyName = memberExpression.Member.Name;
            PropertyInfo property = model.GetProperties().FirstOrDefault(p => p.Name == propertyName);

            if (property == null)
            {
                return;
            }

            DisplayAttribute attr = (DisplayAttribute)property.GetCustomAttributes(false).FirstOrDefault(a => a is DisplayAttribute);
            if (attr == null)
            {
                return;
            }
            htmlAttributes.Merge("placeholder", attr.Description);
        }


        /// <summary>
        /// 表示一个文本框，会根据 DisplayAttribute 特性的 Description 属性，在文本框中生成 placeholder 属性，并加入 bootstrap 的特定文本框样式 "form-control"。
        /// </summary>
        /// <typeparam name="TModel">模型的类型</typeparam>
        /// <typeparam name="TProperty">值的类型</typeparam>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="expression"> 一个表达式，用于标识包含要呈现的属性的对象。</param>
        /// <param name="size">控件尺寸</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个 HTML input 元素，其 type 特性针对表达式表示的对象中的每个属性均设置为“text”。</returns>
        public static MvcHtmlString BootstrapTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, SizeTypes size = SizeTypes.Normal, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.AddOrMerge("class", " form-control ");
            attributes.BuildSizeClass("input", size);
            attributes.BuildPlaceHolder(expression);
            return htmlHelper.TextBoxFor(expression, attributes);
        }

        /// <summary>
        ///  通过使用指定的 HTML 帮助器、窗体字段的名称、值和 HTML 特性，返回文本 input 元素，并加入 bootstrap 的特定文本框样式 "form-control"。
        /// </summary>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="name"> 窗体字段的名称和用于查找值的 System.Web.Mvc.ViewDataDictionary 键。</param>
        /// <param name="value">文本 input 元素的值。按此顺序检索值：System.Web.Mvc.ModelStateDictionary 对象、此参数的值、System.Web.Mvc.ViewDataDictionary 对象，最后是 html 特性中的 value 特性。</param>
        /// <param name="size">控件尺寸</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个 input 元素，其 type 特性设置为“text”。</returns>
        public static MvcHtmlString BootstrapTextBox(this HtmlHelper htmlHelper, string name, object value = null, SizeTypes size = SizeTypes.Normal, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.ObjectToDictionary(htmlAttributes);
            attributes.AddOrMerge("class", " form-control ");
            attributes.BuildSizeClass("input", size);

            return htmlHelper.TextBox(name, value, attributes);
        }

        /// <summary>
        /// 表示一个密码框，会根据 DisplayAttribute 特性的 Description 属性，在文本框中生成 placeholder 属性，并加入 bootstrap 的特定文本框样式 "form-control"。
        /// </summary>
        /// <typeparam name="TModel">模型的类型</typeparam>
        /// <typeparam name="TProperty">值的类型</typeparam>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="expression"> 一个表达式，用于标识包含要呈现的属性的对象。</param>
        /// <param name="size">控件尺寸。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个 HTML input 元素，其 type 特性已使用指定的 HTML 特性，针对指定表达式表示的对象中的每个属性均设置为“password”。</returns>
        public static MvcHtmlString BootstrapPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, SizeTypes size = SizeTypes.Normal, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.AddOrMerge("class", " form-control ");
            attributes.BuildSizeClass("input", size);
            attributes.BuildPlaceHolder(expression);
            return htmlHelper.PasswordFor<TModel, TProperty>(expression, attributes);
        }

        /// <summary>
        ///  通过使用指定的 HTML 帮助器、窗体字段的名称、值和 HTML 特性，返回密码 input 元素，并加入bootstrap的特定文本框样式"form-control"
        /// </summary>
        /// <param name="htmlHelper">扩展的实例。</param>
        /// <param name="name"> 窗体字段的名称和用于查找值的 System.Web.Mvc.ViewDataDictionary 键。</param>
        /// <param name="value">密码 input 元素的值。如果未提供此参数的值，则使用 html 特性中的 value 特性来检索值。</param>
        /// <param name="size">控件尺寸</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个 input 元素，其 type 特性设置为“password”。</returns>
        public static MvcHtmlString BootstrapPassword(this HtmlHelper htmlHelper, string name, object value = null, SizeTypes size = SizeTypes.Normal, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.AddOrMerge("class", " form-control ");
            attributes.BuildSizeClass("input", size);
            return htmlHelper.Password(name, value, attributes);
        }

        /// <summary>
        /// 返回一个 HTML label 元素以及由指定表达式表示的属性的属性名称。自动加入Bootstrap的control-label样式class
        /// </summary>
        /// <typeparam name="TModel">模型的类型</typeparam>
        /// <typeparam name="TValue">值的类型</typeparam>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="expression"> 一个表达式，用于标识包含要呈现的属性的对象。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个 HTML label 元素以及由表达式表示的属性的属性名称。</returns>
        public static MvcHtmlString BootstrapLabelFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.AddOrMerge("class", " control-label ");
            return htmlHelper.LabelFor<TModel, TValue>(expression, attributes);
        }

        /// <summary>
        /// 返回一个 HTML label 元素以及由指定表达式表示的属性的属性名称。自动加入Bootstrap的control-label样式class
        /// </summary>
        /// <param name="htmlHelper"> 此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="expression">一个表达式，用于标识要显示的属性。</param>
        /// <param name="labelText">标签文本。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns> 一个 HTML label 元素以及由表达式表示的属性的属性名称。</returns>
        public static MvcHtmlString BootstrapLabel(this HtmlHelper htmlHelper, string expression, string labelText = null, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.AddOrMerge("class", " control-label ");
            return htmlHelper.Label(expression, labelText, htmlAttributes);
        }


        /// <summary>
        /// 表示一个多行文本框，会根据DisplayAttribute特性的Description属性，在文本框中生成placeholder属性，并加入bootstrap的特定文本框样式"form-control"
        /// </summary>
        /// <typeparam name="TModel">模型的类型</typeparam>
        /// <typeparam name="TProperty">值的类型</typeparam>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="expression"> 一个表达式，用于标识包含要呈现的属性的对象。</param>
        /// <param name="rows">初始化多行文本框高度，默认为3行。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个由表达式表示的对象中的每个属性所对应的 HTML textarea 元素。</returns>
        public static MvcHtmlString BootstrapTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int rows = 3, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.AddOrMerge("class", " form-control ");
            attributes.AddOrMerge("rows", rows);
            attributes.BuildPlaceHolder(expression);
            return htmlHelper.TextAreaFor(expression, attributes);
        }

        /// <summary>
        /// 通过使用指定的 HTML 帮助器、窗体字段的名称、文本内容和指定的 HTML 特性，返回指定的 textarea 元素。并加入bootstrap的特定文本框样式"form-control"
        /// </summary>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="name">要返回的窗体字段的名称。</param>
        /// <param name="value">文本内容。</param>
        /// <param name="rows">行数。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns> textarea 元素。</returns>
        public static MvcHtmlString BootstrapTextArea(this HtmlHelper htmlHelper, string name, string value = null, int rows = 3, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.AddOrMerge("class", " form-control ");
            attributes.AddOrMerge("rows", rows);
            return htmlHelper.TextArea(name, value, attributes);
        }

        /// <summary>
        /// 使用指定列表项和 HTML 特性，为由指定表达式表示的对象中的每个属性返回对应的 HTML select 元素并自动添加适用于Bootstrap的class样式。
        /// </summary>
        /// <typeparam name="TModel"> 模型的类型。</typeparam>
        /// <typeparam name="TProperty">值的类型。</typeparam>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="expression">一个表达式，用于标识包含要显示的属性的对象。</param>
        /// <param name="selectList"> 一个用于填充下拉列表的 System.Web.Mvc.SelectListItem 对象的集合。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个由表达式表示的对象中的每个属性所对应的 HTML select 元素。</returns>
        public static MvcHtmlString BootstrapDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.AddOrMerge("class", " form-control ");

            return htmlHelper.DropDownListFor(expression, selectList, attributes);
        }

        /// <summary>
        /// 通过使用指定的 HTML 帮助器、窗体字段的名称、指定列表项、选项标签和指定的 HTML 特性，返回单选 select 元素并自动添加适用于Bootstrap的class样式。
        /// </summary>
        /// <param name="htmlHelper"> 此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="name">要返回的窗体字段的名称。</param>
        /// <param name="selectList">一个用于填充下拉列表的 System.Web.Mvc.SelectListItem 对象的集合。</param>
        /// <param name="optionLabel">默认空项的文本。此参数可以为 null。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个 HTML select 元素，对于列表中的每一项，该元素都包含一个对应的 option 子元素。</returns>
        public static MvcHtmlString BootstrapDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel = null, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.AddOrMerge("class", " form-control ");

            return htmlHelper.DropDownList(name, selectList, optionLabel, attributes);
        }

        /// <summary>
        /// 为由指定表达式表示的枚举中的每个值返回对应的 HTML select 元素并自动添加适用于Bootstrap的class样式。
        /// </summary>
        /// <typeparam name="TModel">模型的类型。</typeparam>
        /// <typeparam name="Enum">值的类型。</typeparam>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="expression">一个表达式，用于标识包含要显示的值的对象。</param>
        /// <param name="optionLabel"> 默认空项的文本。此参数可以为 null。</param>
        /// <param name="htmlAttributes"> 一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个由表达式表示的枚举中的每个值所对应的 HTML select 元素。</returns>
        public static MvcHtmlString BootstrapEnumDropDownListFor<TModel, Enum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, Enum>> expression, string optionLabel = null, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.AddOrMerge("class", " form-control ");

            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string name = ExpressionHelper.GetExpressionText(expression);

            var value = metaData.Model;
            var fields = metaData.ModelType.GetFields();
            IEnumerable<SelectListItem> selectList = metaData.ModelType.EnumToDropDownList(value);

            return htmlHelper.DropDownList(name, selectList, optionLabel, attributes);
        }

        /// <summary>
        /// 为由指定表达式表示的枚举中的每个值返回对应的 HTML select 元素并自动添加适用于Bootstrap的class样式。
        /// </summary>
        /// <typeparam name="TEnum">一个枚举类型</typeparam>
        /// <param name="htmlHelper"> 此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="name">要返回的窗体字段的名称。</param>
        /// <param name="value">一个在SelectListItem中被选中的值。</param>
        /// <param name="optionLabel">默认空项的文本。此参数可以为 null。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个 HTML select 元素，对于列表中的每一项，该元素都包含一个对应的 option 子元素。</returns>
        public static MvcHtmlString BootstrapEnumDropDownList<TEnum>(this HtmlHelper htmlHelper, string name, object value, string optionLabel = null, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.AddOrMerge("class", " form-control ");

            IEnumerable<SelectListItem> selectList = typeof(TEnum).EnumToDropDownList(value);

            return htmlHelper.DropDownList(name, selectList, optionLabel, attributes);
        }

        /// <summary>
        /// 返回一个 HTML checkbox 元素以及由指定表达式表示的属性的属性名称。
        /// </summary>
        /// <typeparam name="TModel">模型的类型</typeparam>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="expression"> 一个表达式，用于标识包含要呈现的属性的对象。</param>
        /// <param name="inline">获取一个布尔值，表示是否使用 "inline" 样式。若为 true，则会沾满一行的空间。可参考 bootstrap 文档。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个 HTML checkbox 元素以及由表达式表示的属性的属性名称。</returns>
        public static MvcHtmlString BootstrapCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, bool inline = false, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            TagBuilder tag = new TagBuilder("div");
            tag.AddCssClass(inline ? "checkbox" : "checkbox-inline");

            string text = String.Empty;
            DisplayAttribute displayAttr = (expression.Body as MemberExpression).Member.GetCustomAttribute<DisplayAttribute>();
            if (displayAttr != null)
            {
                text = displayAttr.Name;
            }

            var checkbox = htmlHelper.CheckBoxFor(expression);
            tag.InnerHtml = "<label>{0}{1}</label>".StringFormat(checkbox, text);

            tag.MergeAttributes(attributes);
            return MvcHtmlString.Create(tag.ToString());
        }


        /// <summary>
        ///  通过使用指定的 HTML 帮助器、窗体字段的名称、用于指示是否已选中复选框的值以及 HTML 特性，返回复选框 input 元素。
        /// </summary>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="name"> 窗体字段的名称。</param>
        /// <param name="isChecked">如果要选中复选框，则为 true；否则为 false。按此顺序检索复选框的值：System.Web.Mvc.ModelStateDictionary 对象、此参数的值、System.Web.Mvc.ViewDataDictionary 对象，最后是 html 特性中的 checked 特性。</param>
        /// <param name="text">CheckBox后面的文字</param>
        /// <param name="inline">获取一个布尔值，表示是否使用 "inline" 样式。若为 true，则会沾满一行的空间。可参考 bootstrap 文档。</param>
        /// <param name="value">复选框的值</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个 input 元素，其 type 特性设置为“checkbox”。</returns>
        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper htmlHelper, string name, bool isChecked = false, string text = null, object value = null, bool inline = false, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            TagBuilder tag = new TagBuilder("div");
            tag.AddCssClass(inline ? "checkbox" : "checkbox-inline");

            var checkbox = htmlHelper.CheckBox(name, isChecked, htmlAttributes);
            tag.InnerHtml = "<label>{0}{1}</label>".StringFormat(checkbox, text);
            attributes.Add("value", value);
            tag.MergeAttributes(attributes);

            return MvcHtmlString.Create(tag.ToString());
        }


        /// <summary>
        /// 返回一个 HTML radio 元素以及由指定表达式表示的属性的属性名称。
        /// </summary>
        /// <typeparam name="TModel">模型的类型</typeparam>
        /// <typeparam name="TProperty">值的类型</typeparam>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="expression"> 一个表达式，用于标识包含要呈现的属性的对象。</param>
        /// <param name="value">单选按钮所包含的值。</param>
        /// <param name="inline">获取一个布尔值，表示是否使用 "inline" 样式。若为 true，则会沾满一行的空间。可参考 bootstrap 文档。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个 HTML radio 元素以及由表达式表示的属性的属性名称。</returns>
        public static MvcHtmlString BootstrapRadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object value, bool inline = false, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            TagBuilder tag = new TagBuilder("div");
            tag.AddCssClass(inline ? "radio" : "radio-inline");

            string text = String.Empty;
            DisplayAttribute displayAttr = (expression.Body as MemberExpression).Member.GetCustomAttribute<DisplayAttribute>();
            if (displayAttr != null)
            {
                text = displayAttr.Name;
            }
            var radio = htmlHelper.RadioButtonFor(expression, value);
            tag.InnerHtml = "<label>{0}{1}</label>".StringFormat(radio, text);
            tag.MergeAttributes(attributes);
            return MvcHtmlString.Create(tag.ToString());
        }

        /// <summary>
        ///  通过使用指定的 HTML 帮助器、窗体字段的名称、用于指示是否已选中单选框的值以及 HTML 特性，返回单选框 input 元素。
        /// </summary>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="name"> 窗体字段的名称。</param>
        /// <param name="isChecked">如果要选中单选框，则为 true；否则为 false。按此顺序检索单选框的值：System.Web.Mvc.ModelStateDictionary 对象、此参数的值、System.Web.Mvc.ViewDataDictionary 对象，最后是 html 特性中的 checked 特性。</param>
        /// <param name="text">单选框后面的文字</param>
        /// <param name="value">单选框的值</param>
        /// <param name="inline">获取一个布尔值，表示是否使用 "inline" 样式。若为 true，则会沾满一行的空间。可参考 bootstrap 文档。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个 input 元素，其 type 特性设置为“radio”。</returns>
        public static MvcHtmlString BootstrapRadioButton(this HtmlHelper htmlHelper, string name, object value, bool isChecked = false, string text = null, bool inline = false, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            TagBuilder tag = new TagBuilder("div");
            tag.AddCssClass(inline ? "radio" : "radio-inline");

            var radio = htmlHelper.RadioButton(name, value, isChecked, htmlAttributes);
            tag.InnerHtml = "<label>{0}{1}</label>".StringFormat(radio, text);
            tag.MergeAttributes(attributes);

            return MvcHtmlString.Create(tag.ToString());
        }

        /// <summary>
        /// 表示输出一个适用于 Bootstrap 风格的 button 元素。
        /// </summary>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="options">按钮的配置。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个 适用于 Bootstrap 风格的 HTML button 元素。</returns>
        public static MvcHtmlString BootstrapButton(this HtmlHelper htmlHelper, BootstrapButtonOptions options, object htmlAttributes = null)
        {
            TagBuilder buttonTag = new TagBuilder("button");

            var attributes = HtmlHelper.ObjectToDictionary(htmlAttributes);

            attributes.AddOrMerge("class", " btn ");
            if (!options.CssClass.IsNullOrEmpty())
            {
                attributes.AddOrMerge("class", " {0} ".StringFormat(options.CssClass));
            }
            attributes.BuildSizeClass("btn-", options.Size);
            attributes.BuildColorClass("btn-", options.Color);

            if (options.IconDirection == DirectionLayout.Bottom || options.IconDirection == DirectionLayout.Top)
            {
                throw new ArgumentOutOfRangeException("Icon direction only support Left and Right.");
            }

            if (options.IconDirection == DirectionLayout.Left)
            {
                if (!options.IconClass.IsNullOrWhiteSpace())
                {
                    buttonTag.InnerHtml += "<i class=\"{0}\"></i> ".StringFormat(options.IconClass);
                }
            }

            buttonTag.InnerHtml += options.Text;

            if (options.IconDirection == DirectionLayout.Right)
            {
                if (!options.IconClass.IsNullOrWhiteSpace())
                {
                    buttonTag.InnerHtml += "<i class=\"{0}\"></i> ".StringFormat(options.IconClass);
                }
            }

            switch (options.Type)
            {
                case ButtonTypes.Button:
                    attributes.AddIfNotContains("type", "button");
                    break;
                case ButtonTypes.Submit:
                    attributes.AddIfNotContains("type", "button");

                    break;
                case ButtonTypes.Reset:
                    attributes.AddIfNotContains("type", "button");
                    break;
                default:
                    break;
            }

            if (!options.Link.IsNullOrWhiteSpace())
            {
                attributes.AddOrMerge("onclick", " location.href='{0}'; ".StringFormat(options.Link));
            }

            buttonTag.MergeAttributes(attributes);

            return MvcHtmlString.Create(buttonTag.ToString());
        }

        /// <summary>
        /// 输出符合 bootstrap 风格的分页 HTML 一系列相关元素。
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="pagedEntity"><see cref="IPagedCollection{TEntity}"/> 可分页的对象。</param>
        /// <param name="options">分页的配置对象。</param>
        /// <returns></returns>
        public static MvcHtmlString BootstrapPageButton<TEntity>(this HtmlHelper htmlHelper, IPagedCollection<TEntity> pagedEntity, BootstrapPagedButtonOptions options = null)
        {
            if (options == null)
            {
                options = new BootstrapPagedButtonOptions();
            }

            if (pagedEntity.Count == 0 || pagedEntity.ItemsPerPage == 0)
            {
                return MvcHtmlString.Empty;
            }
            pagedEntity.ComputePages();
            var totalPages = pagedEntity.TotalPages;
            if ((pagedEntity.Count % pagedEntity.ItemsPerPage) > 0)
            {
                totalPages++;
            }

            //未超过一页时不显示分页按钮
            if (!options.AlwaysShow && totalPages <= 1)
            {
                return MvcHtmlString.Empty;
            }

            var pageIndex = pagedEntity.CurrentPage;

            bool showNumberic = false;
            bool showFirst = false;
            bool showLast = false;
            bool showPrevious = false;
            bool showNext = false;
            var mode = options.ButtonMode;
            if (mode == PageButtonMode.FirstLastNumberic || mode == PageButtonMode.PreviousNextFirstLastNumberic || mode == PageButtonMode.PreviousNextNumberic || mode == PageButtonMode.OnlyNumberic)
            {
                showNumberic = true;
            }

            if (mode == PageButtonMode.FirstLastNumberic || mode == PageButtonMode.PreviousNextFirstLast || mode == PageButtonMode.PreviousNextFirstLastNumberic)
            {
                showFirst = true;
                showLast = true;
            }

            if (mode == PageButtonMode.PreviousNext || mode == PageButtonMode.PreviousNextFirstLast || mode == PageButtonMode.PreviousNextFirstLastNumberic || mode == PageButtonMode.PreviousNextNumberic)
            {
                showPrevious = true;
                showNext = true;
            }

            string firstText = options.TextOfFirst;
            string previouseText = options.TextOfPrevious;
            string nextText = options.TextOfNext;
            string lastText = options.TextOfLast;

            //分页字符串
            TagBuilder pageBuilder = new TagBuilder("ul");

            pageBuilder.AddCssClass("pagination");

            switch (options.Size)
            {
                case SizeTypes.SM:
                    pageBuilder.AddCssClass(" pagination-sm ");
                    break;
                case SizeTypes.LG:
                    pageBuilder.AddCssClass(" pagination-lg ");
                    break;
                default:
                    break;
            }

            pageBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(options.HtmlAttributes));

            if (showFirst)
            {
                htmlHelper.GeneratePageButton(pageBuilder, pageIndex <= 1, 1, firstText, options);
            }

            if (showPrevious)
            {
                htmlHelper.GeneratePageButton(pageBuilder, pageIndex - 1 < 1, pageIndex - 1, previouseText, options);
            }

            #region 计算页数

            if (showNumberic)
            {
                long startPage = 1;
                long endPage = 1;
                int numbericCount = options.NumbericCount;
                if (totalPages > numbericCount)
                {
                    if (pageIndex - (numbericCount / 2) > 0)
                    {
                        if (pageIndex + (numbericCount / 2) < totalPages)
                        {
                            startPage = pageIndex - (numbericCount / 2);
                            endPage = startPage + numbericCount - 1;
                        }
                        else
                        {
                            endPage = totalPages;
                            startPage = endPage - numbericCount + 1;
                        }
                    }
                    else
                    {
                        endPage = numbericCount;
                    }
                }
                else
                {
                    startPage = 1;
                    endPage = totalPages;
                }

                for (var i = startPage; i <= endPage; i++)
                {
                    htmlHelper.GeneratePageButton(pageBuilder, i == pagedEntity.CurrentPage, i, i.ToString(), options, i == pagedEntity.CurrentPage);
                }
            }
            #endregion

            if (showNext)
            {
                htmlHelper.GeneratePageButton(pageBuilder, pageIndex + 1 > totalPages, pageIndex + 1, nextText, options);
            }

            if (showLast)
            {
                htmlHelper.GeneratePageButton(pageBuilder, pageIndex >= totalPages, totalPages, lastText, options);
            }
            return MvcHtmlString.Create(pageBuilder.ToString());
        }

        /// <summary>
        /// 返回适用于 Bootstrap 中的 System.Web.Mvc.ModelStateDictionary 对象中的验证消息的未排序列表（ul 元素），并使用 alert 样式进行布局。
        /// </summary>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="colorType">输出指定颜色类型的验证信息。</param>
        /// <param name="hasClose">一个布尔值，表示是否输出 "X" 并由关闭当前的 alert 效果。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个字符串，其中包含验证消息的未排序列表（ul 元素）。</returns>
        public static MvcHtmlString BootstrapValidationSummary(this HtmlHelper htmlHelper, ColorTypes colorType = ColorTypes.Danger, bool hasClose = true, object htmlAttributes = null)
        {
            var errorCollection = htmlHelper.ViewData.ModelState["bootstrap-validation-message-{0}".StringFormat(colorType)];
            if (errorCollection != null)
            {
                var errorBuilder = new StringBuilder();
                if (hasClose)
                {
                    errorBuilder.AppendLine("<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden = \"true\"> &times;</span></button>");
                }
                errorBuilder.AppendLine("<ul>");
                foreach (var error in errorCollection.Errors)
                {
                    errorBuilder.AppendLine($"<li><strong>{error.ErrorMessage}</strong></li>");
                }
                errorBuilder.AppendLine("</ul>");
                return htmlHelper.BootstrapAlert(errorBuilder.ToString(), colorType, htmlAttributes);
            }
            return MvcHtmlString.Empty;
        }

        /// <summary>
        /// 为 BootstrapValidationSummary 控件添加相应的消息。当在 View 中使用 Html.BootstrapValidationSummary 时会输出对应颜色的内容。
        /// </summary>
        /// <param name="modelState">被扩展的ModelStateDictionary对象</param>
        /// <param name="colorType">Bootstrap颜色类型，最后将输出同一种颜色的消息</param>
        /// <param name="content">自定义内容。</param>
        public static void AddBootstrapValidationMessage(this ModelStateDictionary modelState, ColorTypes colorType, string content)
        {
            modelState.AddModelError("bootstrap-validation-message-{0}".StringFormat(colorType), content);
        }

        /// <summary>
        /// 表示输出一个适用于 Bootstrap 风格的 div 元素，该元素将使用 alert 作为 class 样式。
        /// </summary>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="color">警告框的颜色风格，参见 Bootstrap 样式。</param>
        /// <param name="content">文本内容。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个 适用于 Bootstrap 风格的 HTML div 元素。</returns>
        public static MvcHtmlString BootstrapAlert(this HtmlHelper htmlHelper, string content, ColorTypes color = ColorTypes.Primary, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.ObjectToDictionary(htmlAttributes);

            attributes.AddOrMerge("class", "alert");
            attributes.BuildColorClass(" alert-", color);

            TagBuilder tag = new TagBuilder("div");
            tag.InnerHtml += content;
            tag.MergeAttributes(attributes);

            return MvcHtmlString.Create(tag.ToString());
        }

        /// <summary>
        /// 表示输出一个适用于 Bootstrap 风格的 img 元素。
        /// </summary>
        /// <param name="htmlHelper">此方法扩展的 HTML 帮助器实例。</param>
        /// <param name="src">图片的 src 链接地址。</param>
        /// <param name="type">图片的显示方式。</param>
        /// <param name="responsive">是否支持响应式布局。如果设置为 true ，则会给 class 增加一个 img-responsive 。</param>
        /// <param name="text">设置 img 元素中的 alt 和 title 属性，帮助加强 html 规范。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns>一个 适用于 Bootstrap 风格的 HTML img 元素。</returns>
        public static MvcHtmlString BootstrapImage(this HtmlHelper htmlHelper, string src, ImageTypes type = ImageTypes.None,
            bool responsive = true, string text = null, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.ObjectToDictionary(htmlAttributes);


            TagBuilder tag = new TagBuilder("img");
            tag.Attributes.Add("src", src);
            tag.Attributes.Add("title", text);
            tag.Attributes.Add("alt", text);

            switch (type)
            {
                case ImageTypes.Rounded:
                    attributes.AddOrMerge("class", " img-rounded ");
                    break;
                case ImageTypes.Circle:
                    attributes.AddOrMerge("class", " img-circle ");
                    break;
                case ImageTypes.Thumbnail:
                    attributes.AddOrMerge("class", " img-thumbnail ");
                    break;
                default:
                    break;
            }

            if (responsive)
            {
                attributes.AddOrMerge("class", " img-responsive ");
            }

            tag.MergeAttributes(attributes);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.SelfClosing));
        }

    }
}
