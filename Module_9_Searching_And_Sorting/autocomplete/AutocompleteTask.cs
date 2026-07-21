using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete;

internal class AutocompleteTask
{
	/// <returns>
	/// Возвращает первую фразу словаря, начинающуюся с prefix.
	/// </returns>
	/// <remarks>
	/// Эта функция уже реализована, она заработает, 
	/// как только вы выполните задачу в файле LeftBorderTask
	/// </remarks>
	public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
	{
		var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
		if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
			return phrases[index];
            
		return null;
	}

	/// <returns>
	/// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
	/// элементов словаря, начинающихся с prefix.
	/// </returns>
	/// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
	public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
	{
		return phrases
			.Skip(LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1)
			.TakeWhile(p => p.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
			.Take(count)
			.ToArray();
	}

	/// <returns>
	/// Возвращает количество фраз, начинающихся с заданного префикса
	/// </returns>
	public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
	{
		return RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count) -
			   LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) - 1;
    }
}

[TestFixture]
public class AutocompleteTests
{
	[Test]
	public void TopByPrefix_IsEmpty_WhenNoPhrases()
	{
		var actualTopWords = AutocompleteTask.GetTopByPrefix(Array.Empty<string>(), "test", 5);
		NUnit.Framework.Legacy.CollectionAssert.IsEmpty(actualTopWords);
	}

	[Test]
	public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
	{
		var phrases = new[] { "apple", "banana", "cherry" };
		var actualCount = AutocompleteTask.GetCountByPrefix(phrases, "");
        NUnit.Framework.Legacy.ClassicAssert.AreEqual(phrases.Length, actualCount);
	}
}