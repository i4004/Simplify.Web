namespace AcspNet.Modules
{
	/// <summary>
	/// Represent string table
	/// </summary>
	public interface IStringTable : IHideObjectMembers
	{
		/// <summary>
		/// String table items
		/// </summary>
		dynamic Items { get; }

		/// <summary>
		/// Get enum associated value from string table by enum type + enum element name
		/// </summary>
		/// <typeparam name="T">Enum</typeparam>
		/// <param name="enumValue">Enum value</param>
		/// <returns>associated value</returns>
		string GetAssociatedValue<T>(T enumValue) where T : struct;
	}
}