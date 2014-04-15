using System;

namespace AcspNet
{
	public class DefaultDependencyResolver : IDependecyResolver
	{
		public object Resolve(Type type)
		{
			return Activator.CreateInstance(type);
		}
	}
}