using ApplicationHelper;

namespace AcspNet.Extensions
{
	public interface IExtensions : IHideObjectMembers
	{
		IMessagePage MessagePage { get; }
		IIdProcessor IdProcessor { get; }
	}
}