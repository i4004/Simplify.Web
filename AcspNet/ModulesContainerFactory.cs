using AcspNet.Html;
using AcspNet.Identity;

namespace AcspNet
{
	/// <summary>
	/// Source container creation factory
	/// </summary>
	internal class ModulesContainerFactory
	{
		private readonly IAcspContext _acspContext;
		private readonly IAcspSettings _settings;

		internal ModulesContainerFactory(IAcspContext acspContext, IAcspSettings settings)
		{
			_acspContext = acspContext;
			_settings = settings;
		}

		/// <summary>
		/// Creates the source container.
		/// </summary>
		/// <returns></returns>
		public ModulesContainer CreateContainer()
		{
			var container = new ModulesContainer
			{
				Context = _acspContext,
				Environment = new Environment(_acspContext.SitePhysicalPath, _settings),
				LanguageManager = new LanguageManager(_settings.DefaultLanguage, _acspContext.Request.Cookies, _acspContext.Response.Cookies),
			};

			container.FileReader = new FileReader(container.Environment.DataPath, _acspContext.SitePhysicalPath, container.LanguageManager.Language, _settings.DefaultLanguage);
			container.TemplateFactory = new TemplateFactory(container.Environment.TemplatesPhysicalPath, container.LanguageManager.Language, _settings.DefaultLanguage, container.Environment.TemplatesMemoryCache);
			container.StringTable = new StringTable(container.FileReader);
			container.DataCollector = new DataCollector(container.Environment.MainContentVariableName, container.Environment.TitleVariableName, container.StringTable);

			var htmlWrapper = new HtmlWrapper
			{
				ListsGenerator = new ListsGenerator(container.StringTable)
			};

			container.MessageBox = new MessageBox(container.TemplateFactory, container.StringTable, container.DataCollector);

			container.Html = htmlWrapper;

			container.Authentication = new Authentication(_acspContext.Session, _acspContext.Request.Cookies,
				_acspContext.Response.Cookies);

			container.IdVerifier = new IdVerifier(_acspContext.QueryString, _acspContext.Form, container.MessageBox);
			container.MessagePage = new MessagePage(container.Navigator, _acspContext.Session);

			return container;
		}
	}
}