using AcspNet.Authentication;
using AcspNet.Extensions;
using AcspNet.Html;

using ApplicationHelper;

namespace AcspNet
{
	public abstract class ExtensionsBase : IHideObjectMembers
	{
		internal IManager ManagerInstance;
		internal ITemplateFactory TemplateFactoryInstance;
		internal IStringTable StringTableInstance;
		internal IDataCollector DataCollectorInstance;
		internal IEnvironment EnvironmentInstance;
		internal IExtensionsDataLoader ExtensionsDataLoaderInstance;
		internal IHtml HtmlInstance;
		internal IAuthenticationModule AuthenticationModuleInstance;
		internal IExtensions ExtensionsInstance;

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

		public IAuthenticationModule AuthenticationModule
		{
			get { return AuthenticationModuleInstance; }
		}

		public IExtensions Extensions
		{
			get { return ExtensionsInstance; }
		}

		
	}
}