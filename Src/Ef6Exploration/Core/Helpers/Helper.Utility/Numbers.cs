using System.Numerics;

namespace Helper.Utility;

public static class Numbers
{
	public static bool IsBetween(int num, int start, int end)
	{
		return num >= start && num <= end;
	}

	public static long GenerationRandomLong(long min, long max)
	{
		var rand = new Random();
		var buf = new byte[8];

		rand.NextBytes(buf);

		var longRand = BitConverter.ToInt64(buf, 0);

		return (Math.Abs(longRand % (max - min)) + min);
	}

	public static Guid ToGuid(this BigInteger value)
	{
		var bytes = new byte[16];

		value.ToByteArray().CopyTo(bytes, 0);

		return new Guid(bytes);
	}
}