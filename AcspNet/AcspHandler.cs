﻿using System;
using System.Collections.Generic;

namespace AcspNet
{
	///// <summary>
	///// Creates and executes ACSP extensions for current HTTP request
	///// </summary>
	public class AcspHandler// : IAcspProcessor, IAcspProcessorContoller
	{
		//private readonly AcspContext _context;
		private readonly IList<ControllerMetaContainer> _controllersMetaContainers;
		//private readonly IList<LibExtensionMetaContainer> _libExtensionMetaContainers;

		//private readonly IList<ExecExtension> _execExtensionsList;
		//private readonly IList<LibExtension> _libExtensionsList;
		//private readonly Dictionary<Type, bool> _libExtensionsIsInitializedList;

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

		//private bool _isExecutionStopped;

		internal AcspHandler(IList<ControllerMetaContainer> controllersMetaContainers)//, IList<LibExtensionMetaContainer> libExtensionMetaContainers, string currentAction, string currentMode)
		{
			_controllersMetaContainers = controllersMetaContainers;
			//_controllersMetaContainers = new List<ControllerMetaContainer>(controllersMetaContainers.Count);

			//	CreateLibraryExtensionsInstances();
			//	InitializeLibraryExtensions();
			//	RunExecutableExtensions();

			//_execExtensionMetaContainers = execExtensionMetaContainers;
			//_libExtensionMetaContainers = libExtensionMetaContainers;

			CreateControllersInstances();

			//_libExtensionsList = new List<LibExtension>(_libExtensionMetaContainers.Count);
			//_libExtensionsIsInitializedList = new Dictionary<Type, bool>();

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
		}

		//internal IList<LibExtension> LibExtensionsList
		//{
		//	get { return _libExtensionsList; }
		//}

		//internal Dictionary<Type, bool> LibExtensionsIsInitializedList
		//{
		//	get { return _libExtensionsIsInitializedList; }
		//}

		///// <summary>
		///// Creates and executes ACSP extensions for current HTTP request
		///// </summary>
		//public void Execute()
		//{
			//if (!_isExecutionStopped)
			//	_displayer.DisplayNoCache(_pageBuilder.Buid(_dataCollector.Items));

			//	if (Session[IsNewSessionFieldName] == null)
			//		Session.Add(IsNewSessionFieldName, "true");
		//}

		///// <summary>
		///// Stop ACSP execution
		///// </summary>
		//public void StopExecution()
		//{
		//	_isExecutionStopped = true;
		//}

		//private void CreateLibraryExtensionsInstances()
		//{
		//	foreach (var container in _libExtensionMetaContainers)
		//	{
		//		var extension = (LibExtension)Activator.CreateInstance(container.ExtensionType);

		//		//SetExtensionModules(extension);

		//		LibExtensionsList.Add(extension);
		//	}
		//}

		//private void InitializeLibraryExtensions()
		//{
		//	foreach (var extension in LibExtensionsList)
		//	{
		//		extension.Initialize();
		//		LibExtensionsIsInitializedList[extension.GetType()] = true;
		//	}
		//}

		private void CreateControllersInstances()
		{
		//	//ExecExtensionsTypes = new List<Type>(ExecExtensionsMetaContainers.Count);

			//foreach (var container in _execExtensionMetaContainers)
			//{
			//	if ((_context.CurrentAction == "" && _context.CurrentMode == "" && container.RunType == RunType.DefaultPage) ||
			//		(String.Equals(container.Action, _context.CurrentAction, StringComparison.CurrentCultureIgnoreCase) &&
			//		 String.Equals(container.Mode, _context.CurrentMode, StringComparison.CurrentCultureIgnoreCase)) ||
			//		(container.Action == "" && container.RunType == RunType.OnAction))
			//	{


		//			//			SetExtensionModules(extension);

		//			_execExtensionsList.Add(extension);
		//			//			ExecExtensionsTypes.Add(extension.GetType());
		//		}
			//}
		}

		//private void RunExecutableExtensions()
		//{
		//	foreach (var extension in _execExtensionsList)
		//	{
		//		if (!_isExecutionStopped)
		//			extension.Invoke();
		//	}
		//}

		//private void SetExtensionModules(ExtensionBase extension)
		//{
		//	extension.Context = _context;
		//	extension.Processor = this;
		//	extension.ProcessorContoller = this;
		//	extension.Environment = _environment;
		//	extension.TemplateFactory = _templateFactory;
		//	extension.DataCollector = _dataCollector;
		//	extension.ExtensionsDataLoader = _dataLoader;
		//	extension.StringTable = _stringTable;
		//	extension.Html = _htmlWrapper;
		//	extension.AuthenticationModule = _authenticationModule;
		//	extension.Extensions = _extensionsWrapper;		
		//}
	}
}