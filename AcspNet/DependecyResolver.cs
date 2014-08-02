using DryIoc;

namespace AcspNet
{
	/// <summary>
	/// AcspNet IOC container, ambient context to register/resolve objects via DI
	/// </summary>
	public class DependencyResolver
	{
		private static readonly Container ContainerInstance = new Container();

		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <value>
		/// The container.
		/// </value>
		public static Container Container
		{
			get
			{
				return ContainerInstance;
			}		
		}
	}
}