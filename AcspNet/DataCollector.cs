using System.Collections.Generic;
using System.Collections.Specialized;

namespace AcspNet
{
	/// <summary>
	/// Site master page template data collector
	/// </summary>
	public sealed class DataCollector : IDataCollector
	{
		private readonly Manager _manager;

		/// <summary>
		/// Prevent site to be displayed via DataCollector
		/// </summary>
		private bool _isDisplayDisabled;

		internal DataCollector(Manager manager)
		{
			Items = new Dictionary<string, string>();
			_manager = manager;
		}

		public IDictionary<string, string> Items { get; private set; }


		/// <summary>
		/// Prevent site to be displayed via DataCollector
		/// </summary>
		public void DisableSiteDisplay()
		{
			_isDisplayDisabled = true;
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
			Add(_manager.Settings.MainContentVariableName, value);
		}

		/// <summary>
		/// Set template title variable value (all occurrences will be replaced)
		/// </summary>
		/// <param name="value">Value to set</param>
		/// <returns></returns>
		public void AddTitle(string value)
		{
			Add(_manager.Settings.TitleVariableName, value);
		}

		/// <summary>
		/// Set template variable value from StringTable (all occurrences will be replaced)
		/// </summary>
		/// <param name="stringTableKey">StringTable key</param>
		/// <param name="variableName">Variable name in master template file</param>
		/// <returns></returns>
		public void AddSt( string variableName,string stringTableKey)
		{
			Add(variableName, _manager.StringTable[stringTableKey]);
		}

		/// <summary>
		/// Set template main content variable value from StringTable (all occurrences will be replaced)
		/// </summary>
		/// <param name="stringTableKey">StringTable key</param>
		/// <returns></returns>
		public void AddSt(string stringTableKey)
		{
			AddSt(_manager.Settings.MainContentVariableName, stringTableKey);
		}

		/// <summary>
		/// Set template title variable value from StringTable (all occurrences will be replaced)
		/// </summary>
		/// <param name="stringTableKey">StringTable key</param>
		/// <returns></returns>
		public void AddTitleSt(string stringTableKey)
		{
			AddSt(_manager.Settings.TitleVariableName, stringTableKey);
		}

		/// <summary>
		/// Checking if some variable data already in data collector
		/// </summary>
		/// <param name="variableName">Variable name</param>
		public bool IsDataExist(string variableName)
		{
			return Items.ContainsKey(variableName);
		}

		/// <summary>
		/// Combine all collected data and send it to the HTTP response
		/// </summary>
		public void DisplaySite()
		{
			if (!_isDisplayDisabled)
			{
				if (!_manager.Settings.DisableAutomaticSiteTitleSet)
					SetSiteTitle();

				var tpl = _manager.TemplateFactory.Load(_manager.Settings.MasterTemplateFileName);

				foreach (var item in Items.Keys)
					tpl.Set(item, Items[item]);

				_manager.Response.Write(tpl.Get());
			}
		}

		/// <summary>
		/// Write data to the response and stop all extensions execution
		/// </summary>
		/// <param name="data">Data to write</param>
		public void DisplayPartial(string data)
		{
			_manager.Response.Write(data);

			_manager.StopExtensionsExecution();
		}

		private void SetSiteTitle()
		{
			if (string.IsNullOrEmpty(_manager.CurrentAction) && string.IsNullOrEmpty(_manager.CurrentMode)
				&& !IsDataExist(_manager.Settings.TitleVariableName))
				Add(_manager.Settings.TitleVariableName, _manager.StringTable["SiteTitle"]);
			else
				Add(_manager.Settings.TitleVariableName, " - " + _manager.StringTable["SiteTitle"]);
		}
	}
}
