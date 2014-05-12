using AcspNet.Modules;
using AcspNet.Modules.Html;
using AcspNet.Modules.Identity;

namespace AcspNet
{
	/// <summary>
	/// Controller base class
	/// </summary>
	public abstract class Controller : ViewAccessor
	{
		/// <summary>
		/// Current HTTP and ACSP context
		/// </summary>
		public virtual IAcspContext Context { get; internal set; }

		/// <summary>
		/// Current request environment data.
		/// </summary>
		public virtual IEnvironment Environment { get; internal set; }

		/// <summary>
		/// Current language manager
		/// </summary>
		public virtual ILanguageManager LanguageManager { get; internal set; }

		/// <summary>
		/// Text and XML files loader.
		/// </summary>
		public virtual IFileReader FileReader { get; internal set; }

		/// <summary>
		/// Web-site master page data collector.
		/// </summary>
		public virtual IDataCollector DataCollector { get; internal set; }
		
		/// <summary>
		/// Text templates loader.
		/// </summary>
		public virtual ITemplateFactory TemplateFactory { get; internal set; }

		/// <summary>
		/// Localizable text items string table.
		/// </summary>
		public virtual IStringTable StringTable { get; internal set; }
		
		/// <summary>
		/// The HTML message box.
		/// </summary>
		public virtual IMessageBox MessageBox { get; internal set; }

		/// <summary>
		/// Controls users login/logout/autnenticate via cookie or session and stores current user name/password/id unformation
		/// </summary>
		public virtual IAuthentication Authentication { get; internal set; }

		/// <summary>
		/// Identifier processor that is used to parse and act on 'ID' field from request query string or form.
		/// </summary>
		public virtual IIdVerifier IdVerifier { get; internal set; }

		/// <summary>
		/// Website navigation manager, controls current user location, link to previous page or link specific page
		/// </summary>
		public virtual INavigator Navigator { get; internal set; }

		/// <summary>
		/// Gets the message page that is used to display messages to user on a separated site page.
		/// </summary>
		public virtual IMessagePage MessagePage { get; internal set; }
		
		/// <summary>
		/// Result for ajax request response
		/// </summary>
		public virtual string AjaxResult { get; set; }

		/// <summary>
		/// Stop subsequent controllers execution and disable display data from data collector
		/// </summary>
		public virtual bool StopExecution { get; internal set; }

		/// <summary>
		/// Invokes the controller.
		/// </summary>
		public virtual void Invoke()
		{
		}
	}
}
