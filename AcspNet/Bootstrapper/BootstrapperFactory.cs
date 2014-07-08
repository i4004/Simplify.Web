namespace AcspNet.Bootstrapper
{
	/// <summary>
	/// AcspNet bootstrapper factory
	/// </summary>
	public class BootstrapperFactory
	{
		/// <summary>
		/// Creates the bootstrapper.
		/// </summary>
		/// <returns></returns>
		public BaseAcspNetBootstrapper CreateBootstrapper()
		{
			return new BaseAcspNetBootstrapper();
		}
	}
}