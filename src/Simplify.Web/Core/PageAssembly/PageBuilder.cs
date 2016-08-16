using Simplify.DI;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Core.PageAssembly
{
	/// <summary>
	/// Provides web-page builder
	/// </summary>
	public class PageBuilder : IPageBuilder
	{
		private readonly ITemplateFactory _templateFactory;
		private readonly IDataCollector _dataCollector;

		/// <summary>
		/// Initializes a new instance of the <see cref="PageBuilder" /> class.
		/// </summary>
		/// <param name="templateFactory">The template factory.</param>
		/// <param name="dataCollector">The data collector.</param>
		public PageBuilder(ITemplateFactory templateFactory, IDataCollector dataCollector)
		{
			_templateFactory = templateFactory;
			_dataCollector = dataCollector;
		}

		/// <summary>
		/// Buids a web page
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <returns></returns>
		public string Build(IDIContainerProvider containerProvider)
		{
			containerProvider.Resolve<IStringTableItemsSetter>().Set();
			containerProvider.Resolve<IContextVariablesSetter>().SetVariables(containerProvider);

			var tpl = _templateFactory.Load(containerProvider.Resolve<IEnvironment>().MasterTemplateFileName);

			foreach (var item in _dataCollector.Items.Keys)
				tpl.Set(item, _dataCollector.Items[item]);

			return tpl.Get();
		}
	}
}