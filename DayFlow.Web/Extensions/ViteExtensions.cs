
using DayFlow.Web.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DayFlow.Web.Extensions
{
    public static class ViteExtensions
    {

        public static IHtmlContent ViteScript(
            this IHtmlHelper html,
            string entry)
        {

            var service =
                html.ViewContext.HttpContext
                .RequestServices
                .GetRequiredService<IViteManifestService>();

            var src =
                service.GetAsset(entry);

            return new HtmlString(
                $"<script type=\"module\" src=\"{src}\"></script>"
            );
        }

        public static IHtmlContent ViteCss(
            this IHtmlHelper html,
            string entry)
        {

            var service =
                html.ViewContext.HttpContext
                .RequestServices
                .GetRequiredService<IViteManifestService>();


            return new HtmlString(
                service.GetCss(entry)
            );
        }
    }
}