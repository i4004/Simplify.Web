using System.Collections.Specialized;

using ApplicationHelper;

namespace AcspNet
{
	public interface IStringTable : IHideObjectMembers
	{
		/// <summary>
		/// List of string table items
		/// </summary>
		StringDictionary Items { get; }

		/// <summary>
		/// List of string table items
		/// </summary>
		/// <param name="key">Item name</param>
		/// <returns>String table item</returns>
		string this[string key] { get; }

		/// <summary>
		/// Get enum associated value from string table by enum type + enum element name
		/// </summary>
		/// <typeparam name="T">Enum</typeparam>
		/// <param name="enumValue">Enum value</param>
		/// <returns>associated value</returns>
		string GetAssociatedValue<T>(T enumValue) where T : struct;
	}
}