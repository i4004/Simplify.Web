
namespace AcspNet.Html
{
	/// <summary>
	/// Various HTML generation classes container
	/// </summary>
	public interface IHtmlWrapper : IHideObjectMembers
	{
		/// <summary>
		/// HTML comboBox lists generator.
		/// </summary>
		IListsGenerator ListsGenerator { get; }
	}
}
