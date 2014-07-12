using System;

namespace AcspNet.Meta
{
	/// <summary>
	/// Creates controller meta-data
	/// </summary>
	public class ControllerMetaDataFactory : IControllerMetaDataFactory
	{
		/// <summary>
		/// Creates the controller meta data.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns></returns>
		public ControllerMetaData CreateControllerMetaData(Type controllerType)
		{
			return new ControllerMetaData(controllerType, GetControllerExecPatameters(controllerType));
		}
		
		private static ControllerExecParameters GetControllerExecPatameters(Type controllerType)
		{
			string route = null;
			var priority = 0;

			var attributes = controllerType.GetCustomAttributes(typeof(RouteAttribute), false);

			if (attributes.Length > 0)
				route = ((RouteAttribute)attributes[0]).Route;

			attributes = controllerType.GetCustomAttributes(typeof(PriorityAttribute), false);

			if (attributes.Length > 0)
				priority = ((PriorityAttribute)attributes[0]).Priority;

			return !string.IsNullOrEmpty(route) || priority != 0
				? new ControllerExecParameters(route, priority)
				: null;
		}
	}
}