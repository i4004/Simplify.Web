using AcspNet.Html;

namespace AcspNet
{
	/// <summary>
	/// View base class 
	/// </summary>
	public abstract class View : ViewAccessor
	{
		/// <summary>
		/// Text templates loader.
		/// </summary>
		public virtual ITemplateFactory TemplateFactory { get; internal set; }

		/// <summary>
		/// Localizable text items string table.
		/// </summary>
		public virtual IStringTable StringTable { get; internal set; }
		
		/// <summary>
		/// Various HTML generation classes
		/// </summary>
		public virtual IHtmlWrapper Html { get; internal set; }
		
		/// <summary>
		/// Site current language, for example: "en", "ru", "de" etc.
		/// </summary>
		public virtual string Language { get; internal set; }

		/// <summary>
		/// Gets the web-site virtual relative path, for example: /site1 if your web-site url is http://yoursite.com/site1/
		/// </summary>
		public virtual string SiteVirtualPath { get; internal set; }

		/// <summary>
		/// Gets the web-site URL, for example: http://yoursite.com/site1/
		/// </summary>
		/// <value>
		/// The site URL.
		/// </value>
		public virtual string SiteUrl { get; internal set; }
	}
}
