using System;
using System.Collections.Generic;
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
		private readonly Dictionary<string, bool> _libExtensionsIsInitializedList;

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

		private bool _isExtensionsExecutionStopped;
		
		/// <summary>
		/// Prevent site to be displayed via DataCollector
		/// </summary>
		private bool _isDisplayDisabled;

		internal AcspProcessor(IAcspSettings settings, AcspContext context, IList<ExecExtensionMetaContainer> execExtensionMetaContainers, IList<LibExtensionMetaContainer> libExtensionMetaContainers)
		{
			_context = context;
			_execExtensionMetaContainers = execExtensionMetaContainers;
			_libExtensionMetaContainers = libExtensionMetaContainers;

			_execExtensionsList = new List<ExecExtension>(_execExtensionMetaContainers.Count);
			_libExtensionsList = new List<LibExtension>(_libExtensionMetaContainers.Count);
			_libExtensionsIsInitializedList = new Dictionary<string, bool>(_libExtensionMetaContainers.Count);

			_environment = new Environment(_context.SitePhysicalPath, settings, _context.Request.Cookies, _context.Response.Cookies);
			_dataLoader = new ExtensionsDataLoader(_environment.ExtensionsDataPath, _context.SitePhysicalPath, _environment.Language, settings.DefaultLanguage);
			_stringTable = new StringTable(_dataLoader);
			_templateFactory = new TemplateFactory(_environment.TemplatesPhysicalPath, _environment.Language, settings.DefaultLanguage, _environment.TemplatesMemoryCache);
			_dataCollector = new DataCollector(_environment.MainContentVariableName, _environment.TitleVariableName, _stringTable);

			_pageBuilder = new PageBuilder(_environment.MasterTemplateFileName, _templateFactory);
			_displayer = new Displayer(_context.Response);
			//			HtmlWrapper = new HtmlWrapper();
			//			AuthenticationModule = new AuthenticationModule(this);
			//			ExtensionsWrapper = new ExtensionsWrapper();
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

			if (!_isDisplayDisabled)
				_displayer.Display(_pageBuilder.Buid(_dataCollector.Items));

			//	if (Session[IsNewSessionFieldName] == null)
			//		Session.Add(IsNewSessionFieldName, "true");
		}

		/// <summary>
		/// Stop ACSP subsequent extensions execution
		/// </summary>
		public void StopExtensionsExecution()
		{
			_isExtensionsExecutionStopped = true;
		}
		
		/// <summary>
		/// Prevent data sent to displayer to be displayed
		/// </summary>
		public void DisableDisplay()
		{
			_isDisplayDisabled = true;
		}

		private void CreateLibraryExtensionsInstances()
		{
			foreach (var container in _libExtensionMetaContainers)
			{
				var extension = (LibExtension)Activator.CreateInstance(container.ExtensionType);
				extension.Context = _context;
				extension.ProcessorContoller = this;
				extension.Environment = _environment;
				extension.TemplateFactory = _templateFactory;
				extension.DataCollector = _dataCollector;
				extension.ExtensionsDataLoader = _dataLoader;
				extension.StringTable = _stringTable;
				//		extension.HtmlInstance = HtmlWrapper;
				//		extension.AuthenticationModuleInstance = AuthenticationModule;
				//extension.ExtensionsInstance = ExtensionsWrapper;

				_libExtensionsList.Add(extension);
				// _libExtensionsIsInitializedList.Add(container.ExtensionType.Name, false); check not need?
			}
		}

		private void InitializeLibraryExtensions()
		{
			foreach (var extension in _libExtensionsList)
			{
				extension.Initialize();
				_libExtensionsIsInitializedList[extension.GetType().Name] = true;
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
					extension.ProcessorContoller = this;
					extension.Environment = _environment;
					extension.TemplateFactory = _templateFactory;
					extension.DataCollector = _dataCollector;
					extension.ExtensionsDataLoader = _dataLoader;
					extension.StringTable = _stringTable;
					//			extension.HtmlInstance = HtmlWrapper;
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
				if (!_isExtensionsExecutionStopped)
					extension.Invoke();
			}
		}
	}
}