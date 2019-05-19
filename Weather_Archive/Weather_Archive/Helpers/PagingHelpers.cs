using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Weather_Archive.Models;

namespace Weather_Archive.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfoMonth pageInfoMonth, Func<int, int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < pageInfoMonth.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i, PageInfoYear.chosenYear));
                tag.InnerHtml = ((Months)i).ToString();
                // если текущая страница, то выделяем ее,
                // например, добавляя класс
                if (i == pageInfoMonth.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfoYear pageInfoYear, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < ListOfYears.Years.Count; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = ListOfYears.Years[i].year.ToString();
                // если текущая страница, то выделяем ее,
                // например, добавляя класс
                if (i == pageInfoYear.currentYear)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}