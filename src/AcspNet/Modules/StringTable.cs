using System;

namespace AcspNet.Modules
{
	/// <summary>
	/// Localizable text items string table.
	/// </summary>
	public sealed class StringTable : IStringTable
	{
		private readonly IFileReader _fileReader;

		/// <summary>
		/// Load string table with current language
		/// </summary>
		/// <param name="defaultLanguage">The default language.</param>
		/// <param name="fileReader">The file reader.</param>
		public StringTable(string defaultLanguage, IFileReader fileReader)
		{
			_fileReader = fileReader;

			Load();
		}

		/// <summary>
		/// Loads string table.
		/// </summary>
		private void Load()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// String table items
		/// </summary>
		public dynamic Items { get; private set; }

		/// <summary>
		/// Get enum associated value from string table by enum type + enum element name
		/// </summary>
		/// <typeparam name="T">Enum</typeparam>
		/// <param name="enumValue">Enum value</param>
		/// <returns>associated value</returns>
		public string GetAssociatedValue<T>(T enumValue) where T : struct
		{
			throw new NotImplementedException();
		}
	}
}