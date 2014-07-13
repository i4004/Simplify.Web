namespace AcspNet
{
	/// <summary>
	/// Creates and executes controllers for current request
	/// </summary>
	public class ControllersHandler : IControllersHandler
	{
		private readonly IControllerFactory _controllerFactory;

		public ControllersHandler(IControllerFactory controllerFactory)
		{
			_controllerFactory = controllerFactory;
		}

		/// <summary>
		/// Creates and invokes controllers instances.
		/// </summary>
		public void Execute(string route)
		{
			//foreach (var controllerMetaData in _controllersMetaStore.GetControllersMetaData())
			//{
			//	if (IsControllerCanBeExecutedOnCurrentPage(controllerMetaData, route))
			//	{
			//		var controller = _controllerFactory.CreateController(controllerMetaData.ControllerType);
			//		controller.Invoke();
			//	}
			//}
		}

		///// <summary>
		///// Determines whether controller can be executed on current page.
		///// </summary>
		///// <param name="metaData">The controller meta data.</param>
		///// <returns></returns>
		//public bool IsControllerCanBeExecutedOnCurrentPage(IControllerMetaData metaData, string route)
		//{
		//	if (metaData.ExecParameters == null)
		//		return true;

		//	if (metaData.ExecParameters.Route == null)
		//		return true;

		//	return _routeMatcher.Match(route, metaData.ExecParameters.Route).Success;
		//}
	}
}