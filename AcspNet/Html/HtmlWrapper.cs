namespace AcspNet.Html
{
	/// <summary>
	/// Various HTML generation classes container
	/// </summary>
	public sealed class HtmlWrapper : IHtml
	{
		internal IListsGenerator ListsGeneratorInstance;
		internal IMessageBox MessageBoxInstance;

		/// <summary>
		/// HTML comboBox lists generator.
		/// </summary>
		public IListsGenerator ListsGenerator
		{
			get { return ListsGeneratorInstance; }
		}

		/// <summary>
		/// The HTML message box.
		/// </summary>
		public IMessageBox MessageBox
		{
			get { return MessageBoxInstance; }
		}
	}
}