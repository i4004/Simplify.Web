using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.XPath;
using Simplify.Xml;

namespace AcspNet.Modules
{
	/// <summary>
	/// Localizable text items string table.
	/// </summary>
	public sealed class StringTable : IStringTable
	{
		private readonly IList<string> _stringTableFiles;
		private readonly string _defaultLanguage;
		private readonly ILanguageManagerProvider _languageManagerProvider;
		private ILanguageManager _languageManager;
		private readonly IFileReader _fileReader;

		/// <summary>
		/// Load string table with current language
		/// </summary>
		/// <param name="stringTableFiles">The string table files.</param>
		/// <param name="defaultLanguage">The default language.</param>
		/// <param name="languageManagerProvider">The language manager provider.</param>
		/// <param name="fileReader">The file reader.</param>
		public StringTable(IList<string> stringTableFiles, string defaultLanguage, ILanguageManagerProvider languageManagerProvider, IFileReader fileReader)
		{
			_stringTableFiles = stringTableFiles;
			_defaultLanguage = defaultLanguage;
			_languageManagerProvider = languageManagerProvider;
			_fileReader = fileReader;
		}

		/// <summary>
		/// String table items
		/// </summary>
		public dynamic Items { get; private set; }

		/// <summary>
		/// Setups this string table.
		/// </summary>
		public void Setup()
		{
			_languageManager = _languageManagerProvider.Get();

			Reload();
		}

		/// <summary>
		/// Reload string table
		/// </summary>
		public void Reload()
		{
			IDictionary<string, Object> currentItems = new ExpandoObject();
			Items = currentItems;

			foreach (var file in _stringTableFiles)
				Load(file, _defaultLanguage, _languageManager.Language, _fileReader, currentItems);
		}

		/// <summary>
		/// Get enum associated value from string table by enum type + enum element name
		/// </summary>
		/// <typeparam name="T">Enum</typeparam>
		/// <param name="enumValue">Enum value</param>
		/// <returns>associated value</returns>
		public string GetAssociatedValue<T>(T enumValue) where T : struct
		{
			var currentItems = (IDictionary<string, Object>) Items;
			var enumItemName = enumValue.GetType().Name + "." + Enum.GetName(typeof (T), enumValue);

			return currentItems.ContainsKey(enumItemName) ? currentItems[enumItemName] as string : null;
		}

		/// <summary>
		/// Gets the item from string table.
		/// </summary>
		/// <param name="itemName">Name of the item.</param>
		/// <returns></returns>
		public string GetItem(string itemName)
		{
			var currentItems = (IDictionary<string, Object>)Items;

			if (currentItems.ContainsKey(itemName))
				return currentItems[itemName] as string;

			return null;
		}

		/// <summary>
		/// Loads string table.
		/// </summary>
		private static void Load(string fileName, string defaultLanguage, string currentLanguage, IFileReader fileReader, IDictionary<string, Object> currentItems)
		{
			var stringTable = fileReader.LoadXDocument(fileName);

			// Loading current culture strings
			if (stringTable != null && stringTable.Root != null)
				foreach (var item in stringTable.Root.XPathSelectElements("item").Where(x => x.HasAttributes))
					currentItems.Add((string)item.Attribute("name"), string.IsNullOrEmpty(item.Value) ? (string)item.Attribute("value") : item.InnerXml().Trim());

			if (currentLanguage == defaultLanguage)
				return;

			// Loading default culture strings

			stringTable = fileReader.LoadXDocument(fileName, defaultLanguage);

			if (stringTable != null && stringTable.Root != null)
				foreach (var item in stringTable.Root.XPathSelectElements("item").Where(x => x.HasAttributes))
					if (!currentItems.ContainsKey((string)item.Attribute("name")))
						currentItems.Add((string)item.Attribute("name"), string.IsNullOrEmpty(item.Value) ? (string)item.Attribute("value") : item.InnerXml().Trim());
		}
	}
}