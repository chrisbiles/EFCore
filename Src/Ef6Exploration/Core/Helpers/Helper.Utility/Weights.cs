namespace Helper.Utility;

public static class Weights
{
	public static double KgToOz(this double weightInKg)
	{
		const double ouncesInKg = 36;
		var result = Math.Ceiling(weightInKg) * ouncesInKg;
		return result;
	}

	public static double OzToKg(this double weightInKg)
	{
		const double ouncesInKg = 36;

		return weightInKg / ouncesInKg;
	}

	public static double LbToOz(this double weightInLb)
	{
		const double ouncesInLb = 16;
		var result = Math.Ceiling(weightInLb) * ouncesInLb;
		return result;
	}

	public static double OzToLb(this double weightInOz)
	{
		const double ouncesInLb = 16;

		return weightInOz / ouncesInLb;
	}
}