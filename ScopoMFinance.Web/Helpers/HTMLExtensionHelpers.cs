using NtitasCommon.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace ScopoMFinance.Web.Helpers
{
    public static class HTMLExtensionHelpers
    {
        /// <summary>
        /// Creates link similar to Html.ActionLink, just correctly sets the sortDir and sortCol parameters and CSS classes
        /// </summary>
        /// <param name="html">Extension instance</param>
        /// <param name="linkText">Text to display as the link</param>
        /// <param name="actionName">Action name as in Html.ActionLink</param>
        /// <param name="controllerName">Controller name as in Html.ActionLink</param>
        /// <param name="routeDictionary">RouteDictionary same as in Html.ActionLink</param>
        /// <param name="htmlAttributes">HtmlAttributes dictionary same as in Html.ActionLink</param>
        /// <param name="sortColumn">What sortcol id will this column stand for</param>
        /// <returns>Formatted and styled link</returns>
        public static MvcHtmlString SortableHeaderActionLink(this HtmlHelper html, string linkText, string actionName, string controllerName, object routeDictionary, IDictionary<string, object> htmlAttributes, int sortColumn)
        {
            int querySortDir;
            if (!int.TryParse(((html.ViewBag.sortDir ?? html.ViewContext.HttpContext.Request.QueryString["sortDir"]) ?? "").ToString(), out querySortDir))
                querySortDir = (int)SortDirection.Asc;

            int oppositeQuerySortDir = querySortDir == (int)SortDirection.Asc ? (int)SortDirection.Desc : (int)SortDirection.Asc;

            int querySortCol;
            if (!int.TryParse(((html.ViewBag.sortCol ?? html.ViewContext.HttpContext.Request.QueryString["sortCol"]) ?? "").ToString(), out querySortCol))
                querySortCol = 0;

            RouteValueDictionary newRouteDictionary = new RouteValueDictionary(routeDictionary ?? new { });

            // for "key with multiple values" ends up like "&xxx=x&xxx=xx" in the url, the above code can not translate it into
            // newRouteDictionary.Add("xxx", x);
            // newRouteDictionary.Add("xxx", xx);
            // the key can not be duplicated we will need to do some work around
            // The below code translate it into
            // newRouteDictionary.Add("xxx[0]", x);
            // newRouteDictionary.Add("xxx[1]", xx);
            // it will be like "&xxx[0]=x&xxx[1]=xx" in the query string, this is accepted by the controller action as array as well
            // reference http://stackoverflow.com/questions/27383705/how-to-build-a-routevaluedictionary-when-a-route-has-multiple-values
            List<KeyValuePair<string, object>> multiValueRoutes = newRouteDictionary.Where(x => x.Value != null && x.Value.GetType().IsArray).ToList();
            if (multiValueRoutes.Count() > 0)
            {
                foreach (KeyValuePair<string, object> multiValueRoute in multiValueRoutes)
                {
                    if (multiValueRoute.Value is int[])
                    {
                        int[] values = (int[])multiValueRoute.Value;
                        for (int i = 0; i < values.Length; i++)
                            newRouteDictionary.Add(multiValueRoute.Key + "[" + i + "]", values[i]);
                        newRouteDictionary.Remove(multiValueRoute.Key);
                    }
                    else if (multiValueRoute.Value is string[])
                    {
                        string[] values = (string[])multiValueRoute.Value;
                        for (int i = 0; i < values.Length; i++)
                            newRouteDictionary.Add(multiValueRoute.Key + "[" + i + "]", values[i]);
                        newRouteDictionary.Remove(multiValueRoute.Key);
                    }
                    else if (multiValueRoute.Value is bool[])
                    {
                        bool[] values = (bool[])multiValueRoute.Value;
                        for (int i = 0; i < values.Length; i++)
                            newRouteDictionary.Add(multiValueRoute.Key + "[" + i + "]", values[i]);
                        newRouteDictionary.Remove(multiValueRoute.Key);
                    }
                }
            }

            if (!newRouteDictionary.ContainsKey("sortDir"))
                newRouteDictionary.Add("sortDir", querySortCol == sortColumn ? oppositeQuerySortDir : (int)SortDirection.Asc);
            else
                newRouteDictionary["sortDir"] = querySortCol == sortColumn ? oppositeQuerySortDir : (int)SortDirection.Asc;
            if (!newRouteDictionary.ContainsKey("sortCol"))
                newRouteDictionary.Add("sortCol", sortColumn.ToString());

            string keyword = html.ViewContext.HttpContext.Request.QueryString["keyword"];
            if (keyword != null && !newRouteDictionary.ContainsKey("keyword"))
                newRouteDictionary.Add("keyword", keyword);

            string index = html.ViewContext.HttpContext.Request.QueryString["index"];
            if (index != null && !newRouteDictionary.ContainsKey("index"))
                newRouteDictionary.Add("index", index);

            htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();
            if (htmlAttributes.ContainsKey("class"))
            {
                htmlAttributes["class"] += " " + (querySortCol == sortColumn ? (querySortDir == (int)SortDirection.Asc ? "headerSortUp" : "headerSortDown") : "unsorted-header");
            }
            else
            {
                htmlAttributes.Add("class", querySortCol == sortColumn ? (querySortDir == (int)SortDirection.Asc ? "headerSortUp" : "headerSortDown") : "unsorted-header");
            }

            htmlAttributes.Add("id", string.Format("sortable-header-{0}-{1}", actionName, querySortCol.ToString()));

            if (controllerName == null)
                return LinkExtensions.ActionLink(html, linkText, actionName, controllerName, newRouteDictionary, htmlAttributes);
            else
                return LinkExtensions.ActionLink(html, linkText, actionName, newRouteDictionary, htmlAttributes);
        }

        /// <summary>
        /// Creates link similar to Html.ActionLink, just correctly sets the sortDir and sortCol parameters and CSS classes
        /// </summary>
        /// <param name="html">Extension instance</param>
        /// <param name="linkText">MvcHtmlString for the link text will be decoded</param>
        /// <param name="actionName">Action name as in Html.ActionLink</param>
        /// <param name="controllerName">Controller name as in Html.ActionLink</param>
        /// <param name="routeDictionary">RouteDictionary same as in Html.ActionLink</param>
        /// <param name="htmlAttributes">HtmlAttributes dictionary same as in Html.ActionLink</param>
        /// <param name="sortColumn">What sortcol id will this column stand for</param>
        /// <returns>Formatted and styled link</returns>
        public static MvcHtmlString SortableHeaderActionLink(this HtmlHelper html, MvcHtmlString linkText, string actionName, string controllerName, object routeDictionary, IDictionary<string, object> htmlAttributes, int sortColumn)
        {
            string decodedText = HttpUtility.HtmlDecode(linkText.ToHtmlString());
            return SortableHeaderActionLink(html, decodedText, actionName, controllerName, routeDictionary, htmlAttributes, sortColumn);
        }

        /// <summary>
        /// Creates link similar to Html.ActionLink, just correctly sets the sortDir and sortCol parameters and CSS classes
        /// </summary>
        /// <param name="html">Extension instance</param>
        /// <param name="linkText">Text to display as the link</param>
        /// <param name="actionName">Action name as in Html.ActionLink</param>
        /// <param name="sortColumn">What sortcol id will this column stand for</param>
        /// <returns>Formatted and styled link</returns>
        public static MvcHtmlString SortableHeaderActionLink(this HtmlHelper html, string linkText, string actionName, int sortColumn)
        {
            return SortableHeaderActionLink(html, linkText, actionName, null, null, null, sortColumn);
        }

        /// <summary>
        /// Creates link similar to Html.ActionLink, just correctly sets the sortDir and sortCol parameters and CSS classes
        /// </summary>
        /// <param name="html">Extension instance</param>
        /// <param name="linkText">MvcHtmlString for the link text will be decoded</param>
        /// <param name="actionName">Action name as in Html.ActionLink</param>
        /// <param name="sortColumn">What sortcol id will this column stand for</param>
        /// <returns>Formatted and styled link</returns>
        public static MvcHtmlString SortableHeaderActionLink(this HtmlHelper html, MvcHtmlString linkText, string actionName, int sortColumn)
        {
            string decodedText = HttpUtility.HtmlDecode(linkText.ToHtmlString());
            return SortableHeaderActionLink(html, decodedText, actionName, null, null, null, sortColumn);
        }
    }
}