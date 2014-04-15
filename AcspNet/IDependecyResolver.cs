using System;

namespace AcspNet
{
	public interface IDependecyResolver : IHideObjectMembers
	{
		object Resolve(Type type);
	}
}