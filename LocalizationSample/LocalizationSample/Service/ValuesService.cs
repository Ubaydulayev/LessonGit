using System;
using Microsoft.Extensions.Localization;
namespace LocalizationSample.Service
{
	public class ValuesService
	{
		private readonly IStringLocalizer<ValuesService> _stringLocalizer;

		public ValuesService(IStringLocalizer<ValuesService> stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
		}

		public string Value()
		{
			LocalizedString localized = _stringLocalizer["Data"];

			string data = localized;

			return data;
		}
	}
}

