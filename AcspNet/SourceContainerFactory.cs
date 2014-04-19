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

			//_htmlWrapper = new HtmlWrapper();
			//_htmlWrapper.ListsGenerator = new ListsGenerator(_stringTable);
			//_htmlWrapper.MessageBox = new MessageBox(_templateFactory, _stringTable, _dataCollector);

			//_authenticationModule = new AuthenticationModule(_context.Session, _context.Request.Cookies, _context.Response.Cookies);

			//_pageBuilder = new PageBuilder(_environment.MasterTemplateFileName, _templateFactory);
			//_displayer = new Displayer(_context.Response);

			//_extensionsWrapper = new ExtensionsWrapper();
			//_extensionsWrapper.IdVerifier = new IdVerifier(_context.QueryString, _context.Form, _htmlWrapper.MessageBox, _displayer);
			//_extensionsWrapper.Navigator = new Navigator(_context.Session, _context.Response);

			return container;
		}
	}
}