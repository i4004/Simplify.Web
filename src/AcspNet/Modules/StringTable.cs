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
		/// <summary>
		/// Load string table with current language
		/// </summary>
		/// <param name="defaultLanguage">The default language.</param>
		/// <param name="currentLanguage">The current language.</param>
		/// <param name="fileReader">The file reader.</param>
		public StringTable(string defaultLanguage, string currentLanguage, IFileReader fileReader)
		{
			Load(defaultLanguage, currentLanguage, fileReader);
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
			var currentItems = (IDictionary<string, Object>) Items;
			var enumItemName = enumValue.GetType().Name + "." + Enum.GetName(typeof (T), enumValue);

			return currentItems.ContainsKey(enumItemName) ? currentItems[enumItemName] as string : null;
		}

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
		private void Load(string defaultLanguage, string currentLanguage, IFileReader fileReader)
		{
			IDictionary<string, Object> currentItems = new ExpandoObject();
			Items = currentItems;

			var stringTable = fileReader.LoadXDocument("StringTable.xml");

			// Loading current culture strings
			if (stringTable != null && stringTable.Root != null)
				foreach (var item in stringTable.Root.XPathSelectElements("item").Where(x => x.HasAttributes))
					currentItems.Add((string)item.Attribute("name"), string.IsNullOrEmpty(item.Value) ? (string)item.Attribute("value") : item.InnerXml().Trim());

			if (currentLanguage == defaultLanguage)
				return;

			// Loading default culture strings

			stringTable = fileReader.LoadXDocument("StringTable.xml", defaultLanguage);

			if (stringTable != null && stringTable.Root != null)
				foreach (var item in stringTable.Root.XPathSelectElements("item").Where(x => x.HasAttributes))
					if (!currentItems.ContainsKey((string)item.Attribute("name")))
						currentItems.Add((string)item.Attribute("name"), string.IsNullOrEmpty(item.Value) ? (string)item.Attribute("value") : item.InnerXml().Trim());
		}
	}
}