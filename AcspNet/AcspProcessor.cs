using System;
using System.Collections.Generic;

namespace AcspNet
{
	/// <summary>
	/// Creates and executes ACSP extensions for current HTTP request
	/// </summary>
	public class AcspProcessor : IAcspProcessor, IAcspProcessorContoller
	{
		private readonly IAcspSettings _settings;
		private readonly AcspContext _context;
		private readonly IList<ExecExtensionMetaContainer> _execExtensionMetaContainers;
		private readonly IList<LibExtensionMetaContainer> _libExtensionMetaContainers;

		private readonly IList<ExecExtension> _execExtensionsList;
		private readonly IList<LibExtension> _libExtensionsList;
		private readonly Dictionary<string, bool> _libExtensionsIsInitializedList;

		///// <summary>
		///// Web-site master page data collector.
		///// </summary>
		//private readonly IDataCollector DataCollector;

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

		private bool _isExtensionsExecutionStopped;

		internal AcspProcessor(IAcspSettings settings, AcspContext context, IList<ExecExtensionMetaContainer> execExtensionMetaContainers, IList<LibExtensionMetaContainer> libExtensionMetaContainers)
		{
			_settings = settings;
			_context = context;
			_execExtensionMetaContainers = execExtensionMetaContainers;
			_libExtensionMetaContainers = libExtensionMetaContainers;

			_execExtensionsList = new List<ExecExtension>(_execExtensionMetaContainers.Count);
			_libExtensionsList = new List<LibExtension>(_libExtensionMetaContainers.Count);
			_libExtensionsIsInitializedList = new Dictionary<string, bool>(_libExtensionMetaContainers.Count);

//			DataCollector = new DataCollector();

			//			Environment = new Environment(this);
			//DataLoader = new ExtensionsDataLoader(_settings.DefaultExtensionDataPath,);
			//StringTable = new StringTable(this);
			//TemplateFactory = new TemplateFactory(_settings.this);
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

		private void CreateLibraryExtensionsInstances()
		{
			foreach (var container in _libExtensionMetaContainers)
			{
				var extension = (LibExtension)Activator.CreateInstance(container.ExtensionType);
				extension.Context = _context;
				extension.ProcessorContoller = this;
				extension.TemplateFactory = _templateFactory;
				//extension.DataCollectorInstance = DataCollector;
				//		extension.EnvironmentInstance = Environment;
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
					extension.TemplateFactory = _templateFactory;
					//			extension.DataCollectorInstance = DataCollector;
					//			extension.EnvironmentInstance = Environment;
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

			//			DisplaySite();
		}
	}
}