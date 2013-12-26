using System.Collections.Generic;

namespace AcspNet
{
	/// <summary>
	/// Site master page template data collector
	/// </summary>
	public sealed class DataCollector
	{
		private readonly IDictionary<string, string> _siteData = new Dictionary<string, string>();

		private readonly Manager _manager;

		/// <summary>
		/// Prevent site to be displayed via DataCollector
		/// </summary>
		private bool _isDisplayDisabled;

		public DataCollector(Manager manager)
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
		/// <param name="addType">Value addition type</param>
		/// <returns></returns>
		public void Set(string variableName, string value, DataCollectorAddType addType = DataCollectorAddType.AddNew)
		{
			if (string.IsNullOrEmpty(variableName))
				return;

			if (!_siteData.ContainsKey(variableName))
			{
				_siteData.Add(variableName, value);
				return;
			}

			if (addType == DataCollectorAddType.AddNew)
				throw new AcspNetException("DataCollector variable data already exist, variable: " + variableName);

			switch (addType)
			{
				case DataCollectorAddType.AddFromBegin:
					_siteData[variableName] = value + _siteData[variableName];
					break;

				case DataCollectorAddType.AddFromEnd:
					_siteData[variableName] = _siteData[variableName] + value;
					break;
			}
		}

		/// <summary>
		/// Set template main content variable value (all occurrences will be replaced)
		/// </summary>
		/// <param name="value">Value to set</param>
		/// <param name="addType">Value addition type</param>
		/// <returns></returns>
		public void Set(string value, DataCollectorAddType addType = DataCollectorAddType.AddNew)
		{
			Set(Manager.Settings.MainContentVariableName, value, addType);
		}

		/// <summary>
		/// Set template title variable value (all occurrences will be replaced)
		/// </summary>
		/// <param name="value">Value to set</param>
		/// <param name="addType">Value addition type</param>
		/// <returns></returns>
		public void SetTitle(string value, DataCollectorAddType addType = DataCollectorAddType.AddNew)
		{
			Set(Manager.Settings.TitleVariableName, value, addType);
		}

		/// <summary>
		/// Set template variable value from StringTable (all occurrences will be replaced)
		/// </summary>
		/// <param name="variableName">Variable name in master template file</param>
		/// <param name="stringTableKey">StringTable key</param>
		/// <param name="addType">Value addition type</param>
		/// <returns></returns>
		public void SetSt(string variableName, string stringTableKey,
			DataCollectorAddType addType = DataCollectorAddType.AddNew)
		{
			//Set(variableName, Manager.Get<StringTable>()[stringTableKey], addType);
		}

		/// <summary>
		/// Set template main content variable value from StringTable (all occurrences will be replaced)
		/// </summary>
		/// <param name="stringTableKey">StringTable key</param>
		/// <param name="addType">Value addition type</param>
		/// <returns></returns>
		public void SetSt(string stringTableKey,
			DataCollectorAddType addType = DataCollectorAddType.AddNew)
		{
			SetSt(Manager.Settings.MainContentVariableName, stringTableKey, addType);
		}

		/// <summary>
		/// Set template title variable value from StringTable (all occurrences will be replaced)
		/// </summary>
		/// <param name="stringTableKey">StringTable key</param>
		/// <param name="addType">Value addition type</param>
		/// <returns></returns>
		public void SetTitleSt(string stringTableKey,
			DataCollectorAddType addType = DataCollectorAddType.AddNew)
		{
			SetSt(Manager.Settings.TitleVariableName, stringTableKey, addType);
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
			if (_isDisplayDisabled)
				return;

			//var tpl = _manager.Get<TemplateFactory>().Load(MasterTemplateFileNameInstance);

			//foreach (var item in _siteData.Keys)
			//	tpl.Set(item, _siteData[item]);

			//_manager.Response.Write(tpl.Text);
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

	/// <summary>
	/// Data collector data addition type
	/// </summary>
	public enum DataCollectorAddType
	{
		/// <summary>
		/// Add new item
		/// </summary>
		AddNew = 0,
		/// <summary>
		/// Add data to a variable from the begin
		/// </summary>
		AddFromBegin = 1,
		/// <summary>
		/// Add data to a variable from the end
		/// </summary>
		AddFromEnd = 2
	}
}
