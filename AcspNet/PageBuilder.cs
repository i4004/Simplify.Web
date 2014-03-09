using System;
using System.Collections.Generic;

namespace AcspNet
{
	/// <summary>
	/// Builds (combines) web-site page HTML code
	/// </summary>
	public class PageBuilder : IPageBuilder
	{
		private readonly string _masterTemplateFileName;
		private readonly ITemplateFactory _templateFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="PageBuilder" /> class.
		/// </summary>
		/// <param name="masterTemplateFileName">Name of the master template file.</param>
		/// <param name="templateFactory">The template factory.</param>
		/// <exception cref="System.ArgumentNullException">masterTemplateFileName</exception>
		public PageBuilder(string masterTemplateFileName, ITemplateFactory templateFactory)
		{
			if (string.IsNullOrEmpty(masterTemplateFileName)) throw new ArgumentNullException("masterTemplateFileName");
			if (templateFactory == null) throw new ArgumentNullException("templateFactory");

			_masterTemplateFileName = masterTemplateFileName;
			_templateFactory = templateFactory;
		}

		/// <summary>
		/// Buids a web page
		/// </summary>
		/// <param name="dataItems">The data items which should be inserted into master template file.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public string Buid(IDictionary<string, string> dataItems)
		{
			var tpl = _templateFactory.Load(_masterTemplateFileName);

			foreach (var item in dataItems.Keys)
				tpl.Set(item, dataItems[item]);

			return tpl.Get();
		}
	}
}
