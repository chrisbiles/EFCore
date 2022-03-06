namespace Helper.Utility;

public static class CreditCard
{
	private static readonly Func<char, int> CharToInt = c => c - '0';
	private static readonly Func<int, int> DoubleDigit = n => (n * 2).ToString().Select(CharToInt).Sum();
	private static readonly Func<int, bool> IsOddIndex = index => index % 2 == 0;

	public static bool LuhnCheck(string creditCardNumber)
	{
		var checkSum = creditCardNumber
			.Select(CharToInt)
			.Reverse()
			.Select((digit, index) => IsOddIndex(index) ? digit : DoubleDigit(digit))
			.Sum();

		return checkSum % 10 == 0;
	}
}