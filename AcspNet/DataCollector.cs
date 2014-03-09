using System;
using System.Collections.Generic;
using Simplify.Templates;

namespace AcspNet
{
	/// <summary>
	/// Collects web-site master page data
	/// </summary>
	public sealed class DataCollector : IDataCollector
	{
		private readonly string _mainContentVariableName;
		private readonly string _titleVariableName;
		private readonly IStringTable _stringTable;
		private readonly IDictionary<string, string> _items = new Dictionary<string, string>();

		/// <summary>
		/// Initializes a new instance of the <see cref="DataCollector"/> class.
		/// </summary>
		/// <param name="mainContentVariableName">Name of the main content variable.</param>
		/// <param name="titleVariableName">Name of the title variable.</param>
		/// <param name="stringTable">The string table.</param>
		/// <exception cref="System.ArgumentNullException">
		/// mainContentVariableName
		/// or
		/// titleVariableName
		/// or
		/// stringTable
		/// </exception>
		public DataCollector(string mainContentVariableName, string titleVariableName, IStringTable stringTable)
		{
			if (string.IsNullOrEmpty(mainContentVariableName)) throw new ArgumentNullException("mainContentVariableName");
			if (string.IsNullOrEmpty(titleVariableName)) throw new ArgumentNullException("titleVariableName");
			if (stringTable == null) throw new ArgumentNullException("stringTable");

			_mainContentVariableName = mainContentVariableName;
			_titleVariableName = titleVariableName;
			_stringTable = stringTable;
		}

		/// <summary>
		/// Gets the data collector items which will be inserted into master template file.
		/// </summary>
		public IDictionary<string, string> Items
		{
			get { return _items; }
		}

		/// <summary>
		/// Gets the name of the main content variable.
		/// </summary>
		/// <value>
		/// The name of the main content variable.
		/// </value>
		public string MainContentVariableName
		{
			get { return _mainContentVariableName; }
		}

		/// <summary>
		/// Gets the name of the title variable.
		/// </summary>
		/// <value>
		/// The name of the title variable.
		/// </value>
		public string TitleVariableName
		{
			get { return _titleVariableName; }
		}

		/// <summary>
		/// Gets the string table.
		/// </summary>
		/// <value>
		/// The string table.
		/// </value>
		public IStringTable StringTable
		{
			get { return _stringTable; }
		}

		/// <summary>
		/// List of data collector items
		/// </summary>
		/// <param name="key">Item name</param>
		/// <returns>Data collector item</returns>
		public string this[string key]
		{
			get { return Items[key]; }
		}

		/// <summary>
		///  Set template variable value (all occurrences will be replaced)
		/// </summary>
		/// <param name="variableName">Variable name in master template file</param>
		/// <param name="value">Value to set</param>
		/// <returns></returns>
		public void Add(string variableName, string value)
		{
			if (string.IsNullOrEmpty(variableName))
				return;

			if (!Items.ContainsKey(variableName))
			{
				Items.Add(variableName, value);
				return;
			}

			Items[variableName] += value;
		}

		/// <summary>
		/// Set template main content variable value (all occurrences will be replaced)
		/// </summary>
		/// <param name="value">Value to set</param>
		/// <returns></returns>
		public void Add(string value)
		{
			Add(MainContentVariableName, value);
		}

		/// <summary>
		/// Set template main content variable value with data from template (all occurrences will be replaced)
		/// </summary>
		/// <param name="tpl"></param>
		/// <returns></returns>
		public void Add(ITemplate tpl)
		{
			if(tpl == null)
				return;

			Add(MainContentVariableName, tpl.Get());
		}

		/// <summary>
		/// Set template title variable value (all occurrences will be replaced)
		/// </summary>
		/// <param name="value">Value to set</param>
		/// <returns></returns>
		public void AddTitle(string value)
		{
			Add(TitleVariableName, value);
		}

		/// <summary>
		/// Set template variable value from StringTable (all occurrences will be replaced)
		/// </summary>
		/// <param name="stringTableKey">StringTable key</param>
		/// <param name="variableName">Variable name in master template file</param>
		/// <returns></returns>
		public void AddSt(string variableName, string stringTableKey)
		{
			Add(variableName, StringTable[stringTableKey]);
		}

		/// <summary>
		/// Set template main content variable value from StringTable (all occurrences will be replaced)
		/// </summary>
		/// <param name="stringTableKey">StringTable key</param>
		/// <returns></returns>
		public void AddSt(string stringTableKey)
		{
			AddSt(MainContentVariableName, stringTableKey);
		}

		/// <summary>
		/// Set template title variable value from StringTable (all occurrences will be replaced)
		/// </summary>
		/// <param name="stringTableKey">StringTable key</param>
		/// <returns></returns>
		public void AddTitleSt(string stringTableKey)
		{
			AddSt(TitleVariableName, stringTableKey);
		}

		/// <summary>
		/// Checking if some variable data already in data collector
		/// </summary>
		/// <param name="variableName">Variable name</param>
		public bool IsDataExist(string variableName)
		{
			return Items.ContainsKey(variableName);
		}
	}
}
