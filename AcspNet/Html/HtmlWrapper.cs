namespace AcspNet.Html
{
	/// <summary>
	/// Various HTML generation classes container
	/// </summary>
	public sealed class HtmlWrapper : IHtmlWrapper
	{
		/// <summary>
		/// HTML comboBox lists generator.
		/// </summary>
		public IListsGenerator ListsGenerator { get; internal set; }
	}
}