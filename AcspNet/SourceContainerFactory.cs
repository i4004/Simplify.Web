using AcspNet.Html;
using AcspNet.Identity;

namespace AcspNet
{
	/// <summary>
	/// Source containers creation factory
	/// </summary>
	public class SourceContainerFactory : ISourceContainerFactory
	{
		private readonly IAcspContext _acspContext;
		private readonly IAcspSettings _settings;

		internal SourceContainerFactory(IAcspContext acspContext, IAcspSettings settings)
		{
			_acspContext = acspContext;
			_settings = settings;
		}

		/// <summary>
		/// Creates the source container.
		/// </summary>
		/// <returns></returns>
		public SourceContainer CreateContainer()
		{
			var container = new SourceContainer
			{
				Context = _acspContext,
				Environment = new Environment(_acspContext.SitePhysicalPath, _settings),
				LanguageManager = new LanguageManager(_settings.DefaultLanguage, _acspContext.Request.Cookies, _acspContext.Response.Cookies),
			};

			container.FileReader = new FileReader(container.Environment.DataPath, _acspContext.SitePhysicalPath, container.LanguageManager.Language, _settings.DefaultLanguage); ;
			container.TemplateFactory = new TemplateFactory(container.Environment.TemplatesPhysicalPath, container.LanguageManager.Language, _settings.DefaultLanguage, container.Environment.TemplatesMemoryCache);
			container.StringTable = new StringTable(container.FileReader);
			container.DataCollector = new DataCollector(container.Environment.MainContentVariableName, container.Environment.TitleVariableName, container.StringTable);

			var htmlWrapper = new HtmlWrapper
			{
				ListsGenerator = new ListsGenerator(container.StringTable),
				MessageBox = new MessageBox(container.TemplateFactory, container.StringTable, container.DataCollector)
			};

			container.Html = htmlWrapper;

			container.Authentication = new Authentication(_acspContext.Session, _acspContext.Request.Cookies,
				_acspContext.Response.Cookies);

			container.IdVerifier = new IdVerifier(_acspContext.QueryString, _acspContext.Form, container.Html.MessageBox);

			return container;
		}
	}
}