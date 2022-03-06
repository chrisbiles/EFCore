using Helper.Configuration;

namespace Helper.Utility;

public static class WeightsAndMeasures
{
	public static double DimensionalWeight(int length, int width, int height)
	{
		double initCalc = (length * width * height);

        _ = int.TryParse(Settings.GetSetting("DimensionalWeightDivisor"), out var dimWeightDivisor);

		return Math.Round(initCalc / dimWeightDivisor, 0, MidpointRounding.AwayFromZero);
	}
}