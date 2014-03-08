using System;
using System.Collections.Generic;

namespace AcspNet
{
	/// <summary>
	/// Builds web-site page HTML code
	/// </summary>
	public class PageBuilder : IPageBuilder
	{
		private readonly string _masterTemplateFileName;
		private readonly bool _disableAutomaticSiteTitleSet;

		/// <summary>
		/// Initializes a new instance of the <see cref="PageBuilder"/> class.
		/// </summary>
		/// <param name="masterTemplateFileName">Name of the master template file.</param>
		/// <param name="disableAutomaticSiteTitleSet">if set to <c>true</c> [disable automatic site title set].</param>
		/// <exception cref="System.ArgumentNullException">masterTemplateFileName</exception>
		public PageBuilder(string masterTemplateFileName, bool disableAutomaticSiteTitleSet)
		{
			if (string.IsNullOrEmpty(masterTemplateFileName)) throw new ArgumentNullException("masterTemplateFileName");

			_masterTemplateFileName = masterTemplateFileName;
			_disableAutomaticSiteTitleSet = disableAutomaticSiteTitleSet;			
		}

		/// <summary>
		/// Buids a web page
		/// </summary>
		/// <param name="dataItems">The data items which should be inserted into master template file.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public string Buid(IDictionary<string, string> dataItems)
		{
			throw new System.NotImplementedException();
		}

		//private void SetSiteTitle()
		//{
		//	if (string.IsNullOrEmpty(_manager.CurrentAction) && string.IsNullOrEmpty(_manager.CurrentMode)
		//		&& !IsDataExist(_manager.Environment.TitleVariableName))
		//		Add(_manager.Environment.TitleVariableName, _manager.StringTable["SiteTitle"]);
		//	else
		//		Add(_manager.Environment.TitleVariableName, " - " + _manager.StringTable["SiteTitle"]);
		//}
	}
}

//if (!disableAutomaticSiteTitleSet)
//SetSiteTitle();

//var tpl = _manager.TemplateFactory.Load(_manager.Environment.MasterTemplateFileName);

//		foreach (var item in Items.Keys)
//			tpl.Set(item, Items[item]);


//	_manager.StopExtensionsExecution();

