namespace AcspNet
{
	public class ModulesContainerFactory : IModulesContainerFactory
	{
		private readonly string _sitePhysicalPath;
		private readonly IAcspSettings _settings;
		private readonly string _language;

		internal ModulesContainerFactory(string sitePhysicalPath, IAcspSettings settings, string language)
		{
			_sitePhysicalPath = sitePhysicalPath;
			_settings = settings;
			_language = language;
		}


		///// <summary>
		///// Current request environment data.
		///// </summary>
		//private readonly IEnvironment _environment;

		///// <summary>
		///// Web-site master page data collector.
		///// </summary>
		//private readonly IDataCollector _dataCollector;

		///// <summary>
		///// Text and XML files loader.
		///// </summary>
		//private readonly IExtensionsDataLoader _dataLoader;

		///// <summary>
		///// Localizable text items string table.
		///// </summary>
		//private readonly IStringTable _stringTable;

		///// <summary>
		///// Text templates loader.
		///// </summary>
		//private readonly ITemplateFactory _templateFactory;

		//private readonly IPageBuilder _pageBuilder;
		//private readonly IDisplayer _displayer;

		///// <summary>
		///// Various HTML generation classes
		///// </summary>
		//private readonly HtmlWrapper _htmlWrapper;

		///// <summary>
		///// Interface that is used to control users login/logout/autnenticate via cookie or session and stores current user name/password/id unformation
		///// </summary>
		//private readonly IAuthenticationModule _authenticationModule;

		///// <summary>
		///// Additional extensions
		///// </summary>
		//private readonly ExtensionsWrapper _extensionsWrapper;

		public ModulesContainer CreateContainer()
		{
			var container = new ModulesContainer();
			var env = new Environment(_sitePhysicalPath, _settings);

			var fileReader = new FileReader(env.DataPath, _sitePhysicalPath, _language, _settings.DefaultLanguage);

			//_stringTable = new StringTable(_dataLoader);
			//_templateFactory = new TemplateFactory(_environment.TemplatesPhysicalPath, _environment.Language, settings.DefaultLanguage, _environment.TemplatesMemoryCache);
			//_dataCollector = new DataCollector(_environment.MainContentVariableName, _environment.TitleVariableName, _stringTable);

			//_htmlWrapper = new HtmlWrapper();
			//_htmlWrapper.ListsGenerator = new ListsGenerator(_stringTable);
			//_htmlWrapper.MessageBox = new MessageBox(_templateFactory, _stringTable, _dataCollector);

			//_authenticationModule = new AuthenticationModule(_context.Session, _context.Request.Cookies, _context.Response.Cookies);

			//_pageBuilder = new PageBuilder(_environment.MasterTemplateFileName, _templateFactory);
			//_displayer = new Displayer(_context.Response);

			//_extensionsWrapper = new ExtensionsWrapper();
			//_extensionsWrapper.IdVerifier = new IdVerifier(_context.QueryString, _context.Form, _htmlWrapper.MessageBox, _displayer);
			//_extensionsWrapper.Navigator = new Navigator(_context.Session, _context.Response);

			container.FileReader = fileReader;

			return container;
		}
	}
}