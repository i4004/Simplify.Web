using System.Collections.Generic;

namespace AcspNet.CoreExtensions.Library
{
	/// <summary>
	/// Site master page template data collector
	/// </summary>
	[Priority(-7)]
	[Version("1.1")]
	public sealed class DataCollector : LibExtension
	{
		private readonly IDictionary<string, string> _siteData = new Dictionary<string, string>();

		/// <summary>
		/// Master page template file name
		/// </summary>
		private string _masterTemplateFileName = "Index.tpl";

		/// <summary>
		/// Prevent site to be displayed via DataCollector
		/// </summary>
		private bool _isDisplayDisabled;

		/// <summary>
		/// Gets or sets the master page template file name
		/// </summary>
		/// <value>
		/// The name of the master page template file
		/// </value>
		public string MasterTemplateFileName
		{
			get { return _masterTemplateFileName; }
			set { _masterTemplateFileName = value; }
		}

		/// <summary>
		/// Prevent site to be displayed via DataCollector
		/// </summary>
		public void DisableSiteDisplay()
		{
			_isDisplayDisabled = true;
		}

		/// <summary>
		/// Replace template variable with value (all occurrences will be replaced)
		/// </summary>
		/// <param name="variableName">Variable name in master template file</param>
		/// <param name="value">Replace value</param>
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
		/// Replace template variable with value from StringTable (all occurrences will be replaced)
		/// </summary>
		/// <param name="variableName">Variable name in master template file</param>
		/// <param name="stringTableKey">StringTable key</param>
		/// <param name="addType">Value addition type</param>
		/// <returns></returns>
		public void SetSt(string variableName, string stringTableKey,
			DataCollectorAddType addType = DataCollectorAddType.AddNew)
		{
			Set(variableName, Manager.Get<StringTable>()[stringTableKey], addType);
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

			var tpl = Manager.Get<TemplateFactory>().Load(_masterTemplateFileName);

			foreach (var item in _siteData.Keys)
				tpl.Set(item, _siteData[item]);

			Manager.Response.Write(tpl.Text);
		}

		/// <summary>
		/// Write data to the response and stop all extensions execution
		/// </summary>
		/// <param name="data">Data to write</param>
		public void DisplayPartial(string data)
		{
			Manager.Response.Write(data);

			Manager.StopExtensionsExecution();
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
