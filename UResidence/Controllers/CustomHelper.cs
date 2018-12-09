using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomHelper
{
    public static class CustomHelper
    {
        public static IHtmlString Image(this HtmlHelper helper, string src)
        {
            TagBuilder tb = new TagBuilder("img");
            tb.Attributes.Add("src", VirtualPathUtility.ToAbsolute(src));
            tb.Attributes.Add("height", "250px");
            tb.Attributes.Add("width", "250px");
            return new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
            
        }
    }
}