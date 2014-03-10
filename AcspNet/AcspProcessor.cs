using System;
using System.Collections.Generic;
using AcspNet.Html;
using AcspNet.Meta;

namespace AcspNet
{
	/// <summary>
	/// Creates and executes ACSP extensions for current HTTP request
	/// </summary>
	public class AcspProcessor : IAcspProcessor, IAcspProcessorContoller
	{
		private readonly AcspContext _context;
		private readonly IList<ExecExtensionMetaContainer> _execExtensionMetaContainers;
		private readonly IList<LibExtensionMetaContainer> _libExtensionMetaContainers;

		private readonly IList<ExecExtension> _execExtensionsList;
		private readonly IList<LibExtension> _libExtensionsList;
		private readonly Dictionary<Type, bool> _libExtensionsIsInitializedList;

		/// <summary>
		/// Current request environment data.
		/// </summary>
		private readonly IEnvironment _environment;

		/// <summary>
		/// Web-site master page data collector.
		/// </summary>
		private readonly IDataCollector _dataCollector;

		/// <summary>
		/// Text and XML files loader.
		/// </summary>
		private readonly ExtensionsDataLoader _dataLoader;

		/// <summary>
		/// Localizable text items string table.
		/// </summary>
		private readonly StringTable _stringTable;

		/// <summary>
		/// Text templates loader.
		/// </summary>
		private readonly ITemplateFactory _templateFactory;

		private readonly IPageBuilder _pageBuilder;
		private readonly IDisplayer _displayer;
		
		/// <summary>
		/// Various HTML generation classes
		/// </summary>
		private readonly HtmlWrapper _htmlWrapper;

		private bool _isExecutionStopped;

		internal AcspProcessor(IAcspSettings settings, AcspContext context, IList<ExecExtensionMetaContainer> execExtensionMetaContainers, IList<LibExtensionMetaContainer> libExtensionMetaContainers)
		{
			_context = context;
			_execExtensionMetaContainers = execExtensionMetaContainers;
			_libExtensionMetaContainers = libExtensionMetaContainers;

			_execExtensionsList = new List<ExecExtension>(_execExtensionMetaContainers.Count);
			_libExtensionsList = new List<LibExtension>(_libExtensionMetaContainers.Count);
			_libExtensionsIsInitializedList = new Dictionary<Type, bool>();

			_environment = new Environment(_context.SitePhysicalPath, settings, _context.Request.Cookies, _context.Response.Cookies);
			_dataLoader = new ExtensionsDataLoader(_environment.ExtensionsDataPath, _context.SitePhysicalPath, _environment.Language, settings.DefaultLanguage);
			_stringTable = new StringTable(_dataLoader);
			_templateFactory = new TemplateFactory(_environment.TemplatesPhysicalPath, _environment.Language, settings.DefaultLanguage, _environment.TemplatesMemoryCache);
			_dataCollector = new DataCollector(_environment.MainContentVariableName, _environment.TitleVariableName, _stringTable);
			//			AuthenticationModule = new AuthenticationModule(this);
			//			ExtensionsWrapper = new ExtensionsWrapper();

			_pageBuilder = new PageBuilder(_environment.MasterTemplateFileName, _templateFactory);
			_displayer = new Displayer(_context.Response);

			_htmlWrapper = new HtmlWrapper();

			InitializeHtmlWrapper();
		}

		internal IList<LibExtension> LibExtensionsList
		{
			get { return _libExtensionsList; }
		}

		internal Dictionary<Type, bool> LibExtensionsIsInitializedList
		{
			get { return _libExtensionsIsInitializedList; }
		}

		/// <summary>
		/// Creates and executes ACSP extensions for current HTTP request
		/// </summary>
		public void Execute()
		{
			CreateLibraryExtensionsInstances();
			InitializeLibraryExtensions();

			CreateExecutableExtensionsInstances();
			RunExecutableExtensions();

			if (!_isExecutionStopped)
				_displayer.DisplayNoCache(_pageBuilder.Buid(_dataCollector.Items));

			//	if (Session[IsNewSessionFieldName] == null)
			//		Session.Add(IsNewSessionFieldName, "true");
		}

		/// <summary>
		/// Stop ACSP execution
		/// </summary>
		public void StopExecution()
		{
			_isExecutionStopped = true;
		}

		private void CreateLibraryExtensionsInstances()
		{
			foreach (var container in _libExtensionMetaContainers)
			{
				var extension = (LibExtension)Activator.CreateInstance(container.ExtensionType);
				extension.Context = _context;
				extension.Processor = this;
				extension.ProcessorContoller = this;
				extension.Environment = _environment;
				extension.TemplateFactory = _templateFactory;
				extension.DataCollector = _dataCollector;
				extension.ExtensionsDataLoader = _dataLoader;
				extension.StringTable = _stringTable;
				extension.Html = _htmlWrapper;
				//		extension.AuthenticationModuleInstance = AuthenticationModule;
				//extension.ExtensionsInstance = ExtensionsWrapper;

				LibExtensionsList.Add(extension);
			}
		}

		private void InitializeLibraryExtensions()
		{
			foreach (var extension in LibExtensionsList)
			{
				extension.Initialize();
				LibExtensionsIsInitializedList[extension.GetType()] = true;
			}
		}

		private void CreateExecutableExtensionsInstances()
		{
			//ExecExtensionsTypes = new List<Type>(ExecExtensionsMetaContainers.Count);

			foreach (var container in _execExtensionMetaContainers)
			{
				if ((_context.CurrentAction == "" && _context.CurrentMode == "" && container.RunType == RunType.DefaultPage) ||
					(String.Equals(container.Action, _context.CurrentAction, StringComparison.CurrentCultureIgnoreCase) &&
					 String.Equals(container.Mode, _context.CurrentMode, StringComparison.CurrentCultureIgnoreCase)) ||
					(container.Action == "" && container.RunType == RunType.OnAction))
				{
					var extension = (ExecExtension)Activator.CreateInstance(container.ExtensionType);
					extension.Context = _context;
					extension.Processor = this;
					extension.ProcessorContoller = this;
					extension.Environment = _environment;
					extension.TemplateFactory = _templateFactory;
					extension.DataCollector = _dataCollector;
					extension.ExtensionsDataLoader = _dataLoader;
					extension.StringTable = _stringTable;
					extension.Html = _htmlWrapper;
					//			extension.AuthenticationModuleInstance = AuthenticationModule;
					//			extension.ExtensionsInstance = ExtensionsWrapper;

					_execExtensionsList.Add(extension);
					//			ExecExtensionsTypes.Add(extension.GetType());
				}
			}
		}

		private void RunExecutableExtensions()
		{
			foreach (var extension in _execExtensionsList)
			{
				if (!_isExecutionStopped)
					extension.Invoke();
			}
		}
		
		private void InitializeHtmlWrapper()
		{
			_htmlWrapper.ListsGenerator = new ListsGenerator(_stringTable);
			//HtmlWrapper.MessageBoxInstance = new MessageBox(this);
		}
	}
}