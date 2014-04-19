﻿using System;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.XPath;
using Simplify.Xml;

namespace AcspNet
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
		internal StringTable(IFileReader fileReader)
		{
			_fileReader = fileReader;

			Load();
		}

		/// <summary>
		/// Loads strign table.
		/// </summary>
		public void Load()
		{
			Items = new StringDictionary();

			var stringTable = _fileReader.LoadXDocument("StringTable.xml");

			// Loading current culture strings
			if (stringTable != null)
			{
				if (stringTable.Root != null)
					foreach (var item in stringTable.Root.XPathSelectElements("item").Where(x => x.HasAttributes))
						Items.Add((string)item.Attribute("name"), string.IsNullOrEmpty(item.Value) ? (string)item.Attribute("value") : item.InnerXml().Trim());
			}

			// Loading default culture strings

			if (_fileReader.Language == _fileReader.DefaultLanguage)
				return;

			stringTable = _fileReader.LoadXDocument("StringTable.xml", _fileReader.DefaultLanguage);

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
			return this[enumValue.GetType().Name + "." + Enum.GetName(typeof(T), enumValue)];
		}
	}
}
