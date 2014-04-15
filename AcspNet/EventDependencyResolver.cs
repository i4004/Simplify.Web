using System;

namespace AcspNet
{
	public class EventDependencyResolver : IDependecyResolver
	{
		public delegate object Resolver(Type type);

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