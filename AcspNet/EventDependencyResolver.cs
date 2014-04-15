using System;

namespace AcspNet
{
	public delegate object Resolver(Type type);

	public class EventDependencyResolver : IDependecyResolver
	{
		private event Resolver ResolveMethod;

		public EventDependencyResolver(Resolver resolveMethod)
		{
			ResolveMethod = resolveMethod;
		}

		public object Resolve(Type type)
		{
			return ResolveMethod(type);
		}
	}
}