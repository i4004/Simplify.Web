namespace Simplify.Web.Modules.Html
{
	/// <summary>
	/// Various HTML generation classes container
	/// </summary>
	public interface IHtmlWrapper : IHideObjectMembers
	{
		/// <summary>
		/// Gets the message box.
		/// </summary>
		/// <value>
		/// The message box.
		/// </value>
		IMessageBox MessageBox { get; }

		/// <summary>
		/// HTML ComboBox lists generator.
		/// </summary>
		IListsGenerator ListsGenerator { get; }
	}
}