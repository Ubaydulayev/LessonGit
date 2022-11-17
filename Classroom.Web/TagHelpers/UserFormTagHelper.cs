using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Classroom.Web.TagHelpers
{
    [HtmlTargetElement("userform")]
    public class UserFormTagHelper : TagHelper
    {
        [HtmlAttributeName("username")]
        public ModelExpression? UserName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.PreContent.SetContent($"{UserName?.Model}");
            //output.PreContent.SetHtmlContent("<span>Text</span>");
        }
    }
}
