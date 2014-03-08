using System;
using System.Collections.Generic;

namespace AcspNet
{
	/// <summary>
	/// Creates and executes ACSP extensions for current HTTP request
	/// </summary>
	public class AcspProcessor : IAcspProcessor
	{
		//private readonly IAcspSettings _settings;
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

		private bool _isExtensionsExecutionStopped;

		internal AcspProcessor(/*IAcspSettings settings, */AcspContext context, IList<ExecExtensionMetaContainer> execExtensionMetaContainers, IList<LibExtensionMetaContainer> libExtensionMetaContainers)
		{
//			if (settings == null) throw new ArgumentNullException("settings");
			if (context == null) throw new ArgumentNullException("context");
			if (execExtensionMetaContainers == null) throw new ArgumentNullException("execExtensionMetaContainers");
			if (libExtensionMetaContainers == null) throw new ArgumentNullException("libExtensionMetaContainers");

//			_settings = settings;
			_context = context;
			_execExtensionMetaContainers = execExtensionMetaContainers;
			_libExtensionMetaContainers = libExtensionMetaContainers;

			_execExtensionsList = new List<ExecExtension>(_execExtensionMetaContainers.Count);
			_libExtensionsList = new List<LibExtension>(_libExtensionMetaContainers.Count);
			_libExtensionsIsInitializedList = new Dictionary<string, bool>(_libExtensionMetaContainers.Count);

//			DataCollector = new DataCollector();
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
				//		extension.TemplateFactoryInstance = TemplateFactory;
				//		extension.DataCollectorInstance = DataCollector;
				//		extension.EnvironmentInstance = Environment;
				//		extension.ExtensionsDataLoaderInstance = DataLoader;
				//		extension.StringTableInstance = StringTable;
				//		extension.HtmlInstance = HtmlWrapper;
				//		extension.AuthenticationModuleInstance = AuthenticationModule;
				//		extension.ExtensionsInstance = ExtensionsWrapper;

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
					//			extension.TemplateFactoryInstance = TemplateFactory;
					//			extension.DataCollectorInstance = DataCollector;
					//			extension.EnvironmentInstance = Environment;
					//			extension.ExtensionsDataLoaderInstance = DataLoader;
					//			extension.StringTableInstance = StringTable;
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
				if (_isExtensionsExecutionStopped)
					return;

				extension.Invoke();
			}

			//			DisplaySite();
		}
	}
}