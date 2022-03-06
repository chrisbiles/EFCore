namespace Helper.Utility;

public static class Lists
{
	public static List<List<T>> SplitListIntoChunks<T>(this List<T> list, int chunkSize)
	{
		if (chunkSize <= 0) { throw new ArgumentException("chunkSize must be greater than 0."); }

		var returnList = new List<List<T>>();

		while (list.Count > 0)
		{
			var count = list.Count > chunkSize ? chunkSize : list.Count;
			returnList.Add(list.GetRange(0, count));
			list.RemoveRange(0, count);
		}

		return returnList;
	}

	/// <summary>
	/// Determines whether the collection is null or contains no elements.
	/// </summary>
	/// <typeparam name="T">The IEnumerable type.</typeparam>
	/// <param name="enumerable">The enumerable, which may be null or empty.</param>
	/// <returns>
	///     <c>true</c> if the IEnumerable is null or empty; otherwise, <c>false</c>.
	/// </returns>
	public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
    {
        return enumerable switch
        {
            null => true,
            /* If this is a list, use the Count property for efficiency. 
			   The Count property is O(1) while IEnumerable.Count() is O(N). */
            ICollection<T> collection => collection.Count < 1,
            _ => !enumerable.Any()
        };
    }

	/// <summary>
	/// Determines whether the collection is null or contains no elements.
	/// </summary>
	/// <typeparam name="T">The IEnumerable type.</typeparam>
	/// <param name="collection">The collection, which may be null or empty.</param>
	/// <returns>
	///     <c>true</c> if the IEnumerable is null or empty; otherwise, <c>false</c>.
	/// </returns>
	public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
	{
		if (collection == null)
		{
			return true;
		}

		return collection.Count < 1;
	}
}