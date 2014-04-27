using System;

namespace AcspNet
{
	/// <summary>
	/// View factory
	/// </summary>
	public class ViewFactory : IViewFactory
	{
		private readonly ModulesContainer _sourceContainer;

		internal ViewFactory(ModulesContainer sourceContainer)
		{
			_sourceContainer = sourceContainer;
		}

		/// <summary>
		/// Creates the view.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns></returns>
		public View CreateView(Type controllerType)
		{
			var view = (View)DependencyResolver.Current.Resolve(controllerType);

			view.TemplateFactory = _sourceContainer.TemplateFactory;
			view.StringTable = _sourceContainer.StringTable;
			view.FileReader = _sourceContainer.FileReader;
			view.Html = _sourceContainer.Html;

			view.ViewFactory = this;
			view.Language = _sourceContainer.LanguageManager.Language;
			view.SiteUrl = _sourceContainer.Context.SiteUrl;
			view.SiteVirtualPath = _sourceContainer.Context.SiteVirtualPath;

			return view;
		}
	}
}