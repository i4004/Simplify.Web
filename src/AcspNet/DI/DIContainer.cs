using System;
using AcspNet.DI.DIContainerProvider.DryIoc;

namespace AcspNet.DI
{
	/// <summary>
	/// AcspNet IOC ambient context container to register/resolve objects for DI
	/// </summary>
	public class DIContainer
	{
		private static IDIContainerProvider _current;

		/// <summary>
		/// The IOC container
		/// </summary>
		public static IDIContainerProvider Current
		{
			get
			{
				return _current ?? (_current = new DryIocDIProvider());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
			}
		}
	}
}