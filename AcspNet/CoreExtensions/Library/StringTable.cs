using System;
using System.Collections.Specialized;
using System.Xml;

namespace AcspNet.CoreExtensions.Library
{
	/// <summary>
	/// Provides access to site string table
	/// </summary>
	[Priority(-7)]
	[Version("3.0.4")]
	public sealed class StringTable : LibExtension
	{
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
		/// Initializes the library extension.
		/// </summary>
		public override void Initialize()
		{
			Load();
		}

		/// <summary>
		/// Load or reload string table with current language
		/// </summary>
		public void Load()
		{
			Items = new StringDictionary();
			var ev = Manager.Get<EnvironmentVariables>();
			var loader = Manager.Get<ExtensionsDataLoader>();

			var stringTable = loader.LoadXmlDocument("StringTable.xml");

			// Loading current culture strings
			if (stringTable != null)
			{
				foreach (XmlNode item in stringTable.ChildNodes[1])
				{
					if (item.Attributes != null)
						Items.Add(item.Attributes["name"].InnerText, string.IsNullOrEmpty(item.InnerXml) ? item.Attributes["value"].InnerText : item.InnerXml.Trim());
				}
			}

			// Loading default culture strings

			if (ev.Language == EngineSettings.DefaultLanguage)
				return;
			stringTable = ExtensionsDataLoader.LoadXmlDocument("StringTable.xml", EngineSettings.DefaultLanguage);

			if (stringTable == null)
				return;

			foreach (XmlNode item in stringTable.ChildNodes[1])
			{
				if (item.Attributes == null)
					continue;

				var itemName = item.Attributes["name"].InnerText;

				if (!Items.ContainsKey(itemName))
					Items.Add(item.Attributes["name"].InnerText, string.IsNullOrEmpty(item.InnerXml) ? item.Attributes["value"].InnerText : item.InnerXml.Trim());
			}
		}

		/// <summary>
		/// Get enum associated value from string table by enum type + enum element name
		/// </summary>
		/// <typeparam name="T">Enum</typeparam>
		/// <param name="enumValue">Enum value</param>
		/// <returns>associated value</returns>
		public string GetAssociatedValue<T>(T enumValue) where T : struct
		{
			return this[enumValue.GetType().Name + Enum.GetName(typeof(T), enumValue)] ?? "";
		}
	}
}
