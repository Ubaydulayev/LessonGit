using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Classroom.Web.TagHelpers
{
    [HtmlTargetElement(Attributes = "user-for")]
    public class UserForTagHelper : TagHelper
    {
        public ModelExpression? UserFor { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.PreContent.SetContent($"{UserFor?.Model}");
        }
    }
}
