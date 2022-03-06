using System.Globalization;

namespace Helper.Utility;

public static class Culture
{
	public static List<string> GetCultureList()
	{
		var returnList = new List<string>();
		var specName = "(none)";

		foreach (var ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
		{
			try { specName = CultureInfo.CreateSpecificCulture(ci.Name).Name; }
			// ReSharper disable once EmptyGeneralCatchClause
			catch
			{ }

			returnList.Add($"{ci.Name,-12}{specName,-12}{ci.EnglishName}");
		}

		returnList.Sort();
		return returnList;
	}
}