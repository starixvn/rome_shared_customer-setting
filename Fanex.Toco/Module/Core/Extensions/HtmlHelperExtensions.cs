// --------------------------------------------------------------------------
// <copyright file="HtmlHelperExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/19">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using Fanex.CustomerSetting.Module.Core.Entities;

    /// <summary>
    /// It contains extension to render common html control.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// This function add class date picker into textbox,
        /// client script explore all textbox which have class date picker and use JQUERY UI
        /// lib to render date picker control.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="name">The name of date picker.</param>
        /// <param name="date">The date value.</param>
        /// <param name="minDate">The minimum date.</param>
        /// <param name="maxDate">The maximum date.</param>
        /// <param name="formatDate">The format date.</param>
        /// <param name="unavailDates">The unavailable dates.</param>
        /// <returns>The MVC HTML string.</returns>
        public static MvcHtmlString DatePicker(
            this HtmlHelper htmlHelper,
            string name,
            DateTime? date,
            DateTime? minDate = null,
            DateTime? maxDate = null,
            string formatDate = Constants.SHORT_DATE_FORMAT,
            IEnumerable<DateTime> unavailDates = null)
        {
            if (string.IsNullOrWhiteSpace(formatDate))
            {
                throw new ArgumentNullException("formatDate cannot be null");
            }

            string disabledDate = (unavailDates != null && unavailDates.Count() > 0)
                ? string.Join(",", unavailDates.Select(d => d.ToString(Constants.SHORT_DATE_FORMAT)))
                : string.Empty;

            var textbox = htmlHelper.TextBox(
                name, 
                date.HasValue ? date.Value.ToString(formatDate) : string.Empty,
                new
                {
                    @class = "datepicker",
                    mindate = minDate != null && minDate != DateTime.MinValue ? ((DateTime)minDate).ToString("yyyy/MM/dd") : "null",
                    maxdate = maxDate != null && maxDate != DateTime.MinValue ? ((DateTime)maxDate).ToString("yyyy/MM/dd") : "null",
                    disabledDate = disabledDate
                });

            return MvcHtmlString.Create(textbox.ToString());
        }

        /// <summary>
        ///   <see cref="DatePicker" />
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML Helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="minDate">The minimum date.</param>
        /// <param name="maxDate">The maximum date.</param>
        /// <param name="formatDate">The format date.</param>
        /// <param name="unavailDates">The unavailable dates.</param>
        /// <returns>
        /// The MVC HTML string.
        /// </returns>
        public static MvcHtmlString DatePickerFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            DateTime? minDate = null,
            DateTime? maxDate = null,
            string formatDate = Constants.SHORT_DATE_FORMAT,
             IEnumerable<DateTime> unavailDates = null)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            DateTime? value = DateTime.Now;
            if (metaData.Model is DateTime? || metaData.Model is DateTime)
            {
                value = (DateTime?)metaData.Model;
            }
            else
            {
                value = null;
            }

            return htmlHelper.DatePicker(metaData.PropertyName, value, minDate, maxDate, formatDate, unavailDates);
        }

        /// <summary>
        /// Generate year selection item.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="name">The name of DROPDOWNLIST.</param>
        /// <param name="fromYear">From year has the default value is 2000.</param>
        /// <param name="currentYear">Current year has the default value is 2000.</param>
        /// <returns>
        /// The MVC HTML string.
        /// </returns>
        public static MvcHtmlString YearPicker(this HtmlHelper htmlHelper, string name, int fromYear = 2000, int currentYear = 2000)
        {
            var list = new List<SelectListItem>();
            var endYear = DateTime.Now.Year;

            for (var i = endYear; i >= fromYear; i--)
            {
                list.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = i == currentYear
                });
            }

            var dropDownList = htmlHelper.DropDownList(name, list, new { @class = "yearpicker" });
            return MvcHtmlString.Create(dropDownList.ToString());
        }

        /// <summary>
        /// Generate year selection items.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML Helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="fromYear">Default from year 2000.</param>
        /// <returns>The MVC HTML string.</returns>
        public static MvcHtmlString YearPickerFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            int fromYear = 2000)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var currentYear = (int)metaData.Model;

            return htmlHelper.YearPicker(metaData.PropertyName, fromYear, currentYear);
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name)
        {
            return CheckBoxList(htmlHelper, name, null);
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList)
        {
            return CheckBoxList(htmlHelper, name, selectList, (IDictionary<string, object>)null);
        }

        public static MvcHtmlString CheckBoxList(
            this HtmlHelper htmlHelper,
            string name,
            IEnumerable<SelectListItem> selectList,
            object htmlAttributes)
        {
            return CheckBoxList(htmlHelper, name, selectList, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            return CheckBoxListFor(htmlHelper, expression, null);
        }

        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> selectList)
        {
            return CheckBoxListFor(htmlHelper, expression, selectList, (IDictionary<string, object>)null);
        }

        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> selectList,
            object htmlAttributes)
        {
            return CheckBoxListFor(htmlHelper, expression, selectList, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> selectList,
            IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            return CheckBoxList(htmlHelper, ExpressionHelper.GetExpressionText(expression), selectList, htmlAttributes);
        }

        /// <summary>
        /// Drops down list for.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="selectList">The select list.</param>
        /// <param name="firstItem">The first item.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>The MVC HTML string.</returns>
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(
           this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TProperty>> expression,
           IEnumerable<SelectListItem> selectList,
           SelectListItem firstItem,
           object htmlAttributes)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            if (firstItem != null && firstItem.Value != null)
            {
                List<SelectListItem> list = new List<SelectListItem>();
                firstItem.Selected = metaData == null || metaData.Model == null || firstItem.Value == metaData.Model.ToString();

                list.Add(firstItem);

                if (selectList != null)
                {
                    foreach (SelectListItem item in selectList)
                    {
                        item.Selected = metaData == null || metaData.Model == null || firstItem.Value == metaData.Model.ToString();
                        list.Add(item);
                    }
                }

                selectList = list;
            }

            return SelectExtensions.DropDownListFor(htmlHelper, expression, selectList, htmlAttributes);
        }

        /// <summary>
        /// Drops down list for month.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>The MVC HTML string.</returns>
        public static MvcHtmlString DropDownListForMonth<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            int month = metaData == null ? 1 : metaData.Model == null ? 1 : (int)metaData.Model;
            var newitems = DateTimeFormatInfo
                .InvariantInfo
                .MonthNames
                .Where(m => m.Trim().Length != 0)
                .Select((monthName, index) => new SelectListItem
                {
                    Value = (index + 1).ToString(),
                    Text = monthName,
                    Selected = month == (index + 1)
                });

            return SelectExtensions.DropDownListFor(htmlHelper, expression, newitems, htmlAttributes);
        }

        /// <summary>
        /// Drops down list for year.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>The MVC HTML string.</returns>
        public static MvcHtmlString DropDownListForYear<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            int year = metaData == null ? 1 : metaData.Model == null ? 1 : (int)metaData.Model;
            List<SelectListItem> newitems = new List<SelectListItem>();

            for (var i = 2005; i <= DateTime.Now.Year; i++)
            {
                newitems.Add(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = year == i
                });
            }

            return SelectExtensions.DropDownListFor(htmlHelper, expression, newitems.OrderByDescending(m => m.Value), htmlAttributes);
        }

        public static MvcHtmlString RawWithLineBreak(this HtmlHelper helper, string htmlString)
        {
            return new MvcHtmlString(htmlString.Replace("\n", "<br />"));
        }

        public static string GetFullDomainURL()
        {
            NameValueCollection variables = HttpContext.Current.Request.ServerVariables;

            string protocol = (variables["SERVER_PORT_SECURE"] == "1") ? "https://" : "http://";
            string domain = variables["SERVER_NAME"];
            string port = variables["SERVER_PORT"];
            return protocol + domain + ":" + port + (HttpRuntime.AppDomainAppVirtualPath == "/" ? string.Empty : HttpRuntime.AppDomainAppVirtualPath);
        }

        /// <summary>
        /// Renders the view to string.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="viewData">The view data.</param>
        /// <returns>The MVC HTML string.</returns>
        public static string RenderViewToString(Controller controller, string viewName, object viewData)
        {
            var renderedView = new StringBuilder();
            using (var responseWriter = new StringWriter(renderedView))
            {
                var fakeResponse = new HttpResponse(responseWriter);
                var fakeContext = new HttpContext(HttpContext.Current.Request, fakeResponse);
                var fakeControllerContext = new ControllerContext(
                    new HttpContextWrapper(fakeContext), 
                    controller.ControllerContext.RouteData, 
                    controller.ControllerContext.Controller);

                var oldContext = HttpContext.Current;
                HttpContext.Current = fakeContext;

                using (var viewPage = new ViewPage())
                {
                    var html = new HtmlHelper(CreateViewContext(responseWriter, fakeControllerContext), viewPage);
                    html.RenderPartial(viewName, viewData);
                    HttpContext.Current = oldContext;
                }
            }

            return renderedView.ToString();
        }

        private static ViewContext CreateViewContext(TextWriter responseWriter, ControllerContext fakeControllerContext)
        {
            return new ViewContext(fakeControllerContext, new FakeView(), new ViewDataDictionary(), new TempDataDictionary(), responseWriter);
        }

        /// <summary>
        /// Fake view.
        /// </summary>
        public class FakeView : IView
        {
            public void Render(ViewContext viewContext, TextWriter writer)
            {
                throw new NotImplementedException();
            }
        }
    }
}