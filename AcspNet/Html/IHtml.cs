
namespace AcspNet.Html
{
	/// <summary>
	/// Various HTML generation classes container
	/// </summary>
	public interface IHtml : IHideObjectMembers
	{
		/// <summary>
		/// HTML comboBox lists generator.
		/// </summary>
		IListsGenerator ListsGenerator { get; }

		///// <summary>
		///// The HTML message box.
		///// </summary>
		//IMessageBox MessageBox { get; }
	}
}
