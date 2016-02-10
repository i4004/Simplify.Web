namespace Simplify.Web.Modules.Html
{
	/// <summary>
	/// Various HTML generation classes container
	/// </summary>
	public sealed class HtmlWrapper : IHtmlWrapper
	{
		/// <summary>
		/// Gets the message box.
		/// </summary>
		/// <value>
		/// The message box.
		/// </value>
		public IMessageBox MessageBox { get; internal set; }

		/// <summary>
		/// HTML ComboBox lists generator.
		/// </summary>
		public IListsGenerator ListsGenerator { get; internal set; }
	}
}