using System.Globalization;

namespace Helper.Utility;

public static class DateTimes
{
	public static DateTime SetDateOfBirthValue(this DateTime initialValue)
	{
		var newDob = DateTime.ParseExact($"{initialValue.Month}/{initialValue.Day}/1900", "MM/dd/yyyy", CultureInfo.InvariantCulture);

		return newDob;
	}

	public static DateTime CardExpirationValue(this DateTime initialValue)
	{
		var lastDayOfMonth = DateTime.DaysInMonth(initialValue.Year, initialValue.Month);

		var newCardExpireDate = new DateTime(initialValue.Year, initialValue.Month, lastDayOfMonth, 23, 59, 59);

		if (newCardExpireDate < DateTime.Now)
		{
			throw new ArgumentException("Card expiration date cannot be before today.", nameof(initialValue));
		}

		return newCardExpireDate;
	}

	public static DateTime ConvertDateTimeToEst(this DateTime value)
	{
		return TimeZoneInfo.ConvertTimeFromUtc(value, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
	}
}
