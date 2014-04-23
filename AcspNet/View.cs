using AcspNet.Html;

namespace AcspNet
{
	/// <summary>
	/// View base class 
	/// </summary>
	public abstract class View : IHideObjectMembers
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

		internal virtual IViewFactory ViewFactory { get; set; }

		/// <summary>
		/// Gets library extension instance
		/// </summary>
		/// <typeparam name="T">Library extension instance to get</typeparam>
		/// <returns>Library extension</returns>
		public T GetView<T>()
			where T : View
		{
			var type = typeof(T);

			return (T)ViewFactory.CreateView(type);
		}
	}
}
