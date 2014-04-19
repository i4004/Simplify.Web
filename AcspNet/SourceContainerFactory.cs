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
			var container = new SourceContainer();
			var env = new Environment(_acspContext.SitePhysicalPath, _settings);
			var languageManager = new LanguageManager(_settings.DefaultLanguage, _acspContext.Request.Cookies,
				_acspContext.Response.Cookies);

			var fileReader = new FileReader(env.DataPath, _acspContext.SitePhysicalPath, languageManager.Language, _settings.DefaultLanguage);

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

			container.Context = _acspContext;
			container.FileReader = fileReader;

			return container;
		}
	}
}