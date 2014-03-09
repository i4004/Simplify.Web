namespace AcspNet
{
	/// <summary>
	/// ACSP extension base class
	/// </summary>
	public abstract class ExtensionsBase : IHideObjectMembers
	{
		//internal IHtml HtmlInstance;
		//internal IAuthenticationModule AuthenticationModuleInstance;
		//internal IExtensions ExtensionsInstance;

		/// <summary>
		/// Current HTTP and ACSP context
		/// </summary>
		public IAcspContext Context { get; internal set; }

		/// <summary>
		/// Current ACSP executing processor controller
		/// </summary>
		public IAcspProcessorContoller ProcessorContoller { get; internal set; }

		/// <summary>
		/// Text templates loader.
		/// </summary>
		public ITemplateFactory TemplateFactory { get; internal set; }

		/// <summary>
		/// Localizable text items string table.
		/// </summary>
		public IStringTable StringTable { get; internal set; }

		/// <summary>
		/// Web-site master page data collector.
		/// </summary>
		public IDataCollector DataCollector { get; internal set; }

		/// <summary>
		/// Current request environment data.
		/// </summary>
		public IEnvironment Environment { get; internal set; }

		/// <summary>
		/// Text and XML files loader.
		/// </summary>
		public IExtensionsDataLoader ExtensionsDataLoader { get; internal set; }

		///// <summary>
		///// Various HTML generation classes
		///// </summary>
		//public IHtml Html
		//{
		//	get { return HtmlInstance; }
		//}

		///// <summary>
		///// Interface that is used to control users login/logout/autnenticate via cookie or session and stores current user name/password/id unformation
		///// </summary>
		//public IAuthenticationModule AuthenticationModule
		//{
		//	get { return AuthenticationModuleInstance; }
		//}

		///// <summary>
		///// Additional extensions
		///// </summary>
		//public IExtensions Extensions
		//{
		//	get { return ExtensionsInstance; }
		//}	
	}
}