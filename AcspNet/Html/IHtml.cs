using ApplicationHelper;

namespace AcspNet.Html
{
	public interface IHtml : IHideObjectMembers
	{
		IListsGenerator ListsGenerator { get; }
		IMessageBox MessageBox { get; }
	}
}