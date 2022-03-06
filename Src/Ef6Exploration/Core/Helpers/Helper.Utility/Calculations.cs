namespace Helper.Utility;

public static class Calculations
{
	public static decimal RetailPricing(decimal cost, decimal margin)
	{
		return decimal.Round(cost / (1 - margin), 2);
	}

	public static decimal RetailPricing(decimal cost, decimal margin, int numberOfItems)
	{
		return decimal.Round(cost / (1 - margin) * numberOfItems, 2);
	}

	public static decimal MarkupPricing(decimal cost, decimal markup)
	{
		return decimal.Round(cost * markup + cost, 2);
	}

	public static decimal MarkupPricing(decimal cost, decimal markup, int numberOfItems)
	{
		return decimal.Round((cost * markup + cost) * numberOfItems, 2);
	}

	public static decimal TotalServiceCost(decimal cost, int numberOfItems)
	{
		return decimal.Round(cost * numberOfItems, 2);
	}
}
