using System;

namespace AcspNet
{
	public class DependencyResolver
	{
		private static IDependecyResolver _current;
		public static IDependecyResolver Current
		{
			get { return _current ?? (_current = new DefaultDependencyResolver()); }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_current = value;
			}
		}		 
	}
}