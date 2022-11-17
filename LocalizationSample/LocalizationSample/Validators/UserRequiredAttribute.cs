using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace LocalizationSample.Validators;

public class UserRequiredAttribute : RequiredAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not null)
        {
            return true;
        }

        var culture = CultureInfo.CurrentUICulture;

        return false;
    }
}