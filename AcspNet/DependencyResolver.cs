using System;

namespace AcspNet
{
	/// <summary>
	/// Dependency resolver
	/// </summary>
	public class DependencyResolver
	{
		private static IDependecyResolver _dependencyResolver;

		/// <summary>
		/// Gets or sets the dependency resolver fro container factory.
		/// </summary>
		/// <value>
		/// The dependency resolver.
		/// </value>
		/// <exception cref="System.ArgumentNullException">value</exception>
		public static IDependecyResolver Current
		{
			get { return _dependencyResolver ?? (_dependencyResolver = new DefaultDependencyResolver()); }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_dependencyResolver = value;
			}
		}		 
	}
}