using System.Collections.Generic;

using ApplicationHelper;

namespace AcspNet
{
	/// <summary>
	/// Site master page template data collector
	/// </summary>
	public sealed class DataCollector : IDataCollector
	{
		private readonly IDictionary<string, string> _siteData = new Dictionary<string, string>();

		private readonly Manager _manager;

		/// <summary>
		/// Prevent site to be displayed via DataCollector
		/// </summary>
		private bool _isDisplayDisabled;

		internal DataCollector(Manager manager)
		{
			_manager = manager;
		}

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

			if (!_siteData.ContainsKey(variableName))
			{
				_siteData.Add(variableName, value);
				return;
			}

			_siteData[variableName] += value;
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
			return _siteData.ContainsKey(variableName);
		}

		/// <summary>
		/// Combine all collected data and send it to the HTTP response
		/// </summary>
		public void DisplaySite()
		{
			if (!_isDisplayDisabled)
			{
				var tpl = _manager.TemplateFactory.Load(_manager.Settings.MasterTemplateFileName);

				foreach (var item in _siteData.Keys)
					tpl.Set(item, _siteData[item]);

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
	}
}
