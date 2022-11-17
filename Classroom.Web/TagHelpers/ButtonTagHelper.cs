using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Classroom.Web.TagHelpers;

[HtmlTargetElement(Attributes = "button")]
public class ButtonTagHelper : TagHelper
{
    public enum ButtonType
    {
        Primary,
        Secondary,
        Danger,
        Light
    }

    public ButtonType Button { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.SetAttribute("class", $"btn btn-{Button.ToString().ToLower()}");
    }
}