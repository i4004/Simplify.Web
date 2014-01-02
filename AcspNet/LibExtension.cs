using AcspNet.Html;

using ApplicationHelper;

namespace AcspNet
{
	/// <summary>
	/// Library extension base class 
	/// Base class for ACSP.NET library extensions
	/// </summary>
	public abstract class LibExtension : IHideObjectMembers
	{
		internal IManager ManagerInstance;
		internal ITemplateFactory TemplateFactoryInstance;
		internal IStringTable StringTableInstance;
		internal IDataCollector DataCollectorInstance;
		internal IEnvironment EnvironmentInstance;
		internal IExtensionsDataLoader ExtensionsDataLoaderInstance;
		internal IHtml HtmlInstance;

		/// <summary>
		/// Current AcspNet.Manager instance
		/// </summary>
		public IManager Manager
		{
			get { return ManagerInstance; }
		}

		public ITemplateFactory TemplateFactory
		{
			get { return TemplateFactoryInstance; }
		}

		public IStringTable StringTable
		{
			get { return StringTableInstance; }
		}

		public IDataCollector DataCollector
		{
			get { return DataCollectorInstance; }
		}

		public IEnvironment Environment
		{
			get { return EnvironmentInstance; }
		}

		public IExtensionsDataLoader ExtensionsDataLoader
		{
			get { return ExtensionsDataLoaderInstance; }
		}

		public IHtml Html
		{
			get { return HtmlInstance; }
		}

		/// <summary>
		/// Initializes the library extension.
		/// </summary>
		public virtual void Initialize()
		{			
		}
	}
}
