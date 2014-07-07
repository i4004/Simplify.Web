namespace AcspNet.Bootstrapper
{
	public class BootstrapperFactory
	{
		public BaseAcspNetBootstrapper GetBootstrapper()
		{
			return new BaseAcspNetBootstrapper();
		}
	}
}