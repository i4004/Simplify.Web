using System;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.XPath;

using ApplicationHelper.Extensions.Xml;

namespace AcspNet
{
	/// <summary>
	/// Provides access to site string table
	/// </summary>
	public sealed class StringTable : IStringTable
	{
		private readonly Manager _manager;

		/// <summary>
		/// Load string table with current language
		/// </summary>
		public StringTable(Manager manager)
		{
			_manager = manager;

			Reload();
		}

		public void Reload()
		{
			Items = new StringDictionary();

			var stringTable = _manager.DataLoader.LoadXDocument("StringTable.xml");

			// Loading current culture strings
			if (stringTable != null)
			{
				if (stringTable.Root != null)
					foreach (var item in stringTable.Root.XPathSelectElements("item").Where(x => x.HasAttributes))
						Items.Add((string)item.Attribute("name"), string.IsNullOrEmpty(item.Value) ? (string)item.Attribute("value") : item.InnerXml().Trim());
			}
			
			// Loading default culture strings

			if (_manager.Environment.Language == _manager.Settings.DefaultLanguage)
				return;

			stringTable = _manager.DataLoader.LoadXDocument("StringTable.xml", _manager.Settings.DefaultLanguage);

			if (stringTable != null)
			{
				if (stringTable.Root != null)
					foreach (var item in stringTable.Root.XPathSelectElements("item").Where(x => x.HasAttributes))
						if (!Items.ContainsKey((string) item.Attribute("name")))
							Items.Add((string) item.Attribute("name"), string.IsNullOrEmpty(item.Value) ? (string) item.Attribute("value") : item.InnerXml().Trim());
			}
		}

		/// <summary>
		/// List of string table items
		/// </summary>
		public StringDictionary Items { get; private set; }

		/// <summary>
		/// List of string table items
		/// </summary>
		/// <param name="key">Item name</param>
		/// <returns>String table item</returns>
		public string this[string key]
		{
			get { return Items[key]; }
		}

		/// <summary>
		/// Get enum associated value from string table by enum type + enum element name
		/// </summary>
		/// <typeparam name="T">Enum</typeparam>
		/// <param name="enumValue">Enum value</param>
		/// <returns>associated value</returns>
		public string GetAssociatedValue<T>(T enumValue) where T : struct
		{
			return this[enumValue.GetType().Name + Enum.GetName(typeof(T), enumValue)];
		}
	}
}
