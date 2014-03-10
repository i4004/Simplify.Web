namespace AcspNet.Html
{
	/// <summary>
	/// Various HTML generation classes container
	/// </summary>
	public sealed class HtmlWrapper : IHtml
	{
		/// <summary>
		/// HTML comboBox lists generator.
		/// </summary>
		public IListsGenerator ListsGenerator { get; internal set; }

	//	/// <summary>
	//	/// The HTML message box.
	//	/// </summary>
	//	public IMessageBox MessageBox { get; internal set; }
	}
}