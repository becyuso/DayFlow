using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace DayFlow.Web.Common.Mvc.HtmlHelpers;


public static class DisplayHelperExtensions
{
    public static string DisplayNameFor<TModel, TValue>(
       this IHtmlHelper html,
       Expression<Func<TModel, TValue>> expression)
    {
        if (expression.Body is not MemberExpression member)
            return string.Empty;

        var metadata =
            html.MetadataProvider.GetMetadataForProperty(
                typeof(TModel),
                member.Member.Name);

        return metadata.DisplayName
            ?? metadata.PropertyName
            ?? member.Member.Name;
    }
}

