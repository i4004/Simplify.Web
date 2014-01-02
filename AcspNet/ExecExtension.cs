using AcspNet.Html;

using ApplicationHelper;

namespace AcspNet
{
	/// <summary>
	/// Executable extensions base class
	/// Provides interface for ACSP.NET executable extensions
	/// </summary>
	public abstract class ExecExtension : IHideObjectMembers
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
		/// Invokes the executable extension.
		/// </summary>
		public virtual void Invoke()
		{
		}
	}
}
