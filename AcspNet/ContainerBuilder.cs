namespace AcspNet
{
	public class ContainerBuilder : IContainerBuilder
	{
		private readonly IEnvironment _environment;

		internal ContainerBuilder(IEnvironment environment)
		{
			_environment = environment;
		}

		public void FillContainer(Container container)
		{
			//container.ExtensionsDataLoader
		}
	}
}