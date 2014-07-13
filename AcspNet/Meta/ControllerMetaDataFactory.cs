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
			var priority = 0;

			var attributes = controllerType.GetCustomAttributes(typeof(PriorityAttribute), false);

			if (attributes.Length > 0)
				priority = ((PriorityAttribute)attributes[0]).Priority;

			var routeInfo = GetControllerRouteInfo(controllerType);

			return routeInfo != null || priority != 0
				? new ControllerExecParameters(routeInfo, priority)
				: null;
		}

		private static ControllerRouteInfo GetControllerRouteInfo(Type controllerType)
		{
			string getRoute = null;
			string postRoute = null;
			string putRoute = null;
			string deleteRoute = null;

			var attributes = controllerType.GetCustomAttributes(typeof(GetAttribute), false);

			if (attributes.Length > 0)
				getRoute = ((GetAttribute)attributes[0]).Route;

			attributes = controllerType.GetCustomAttributes(typeof(PostAttribute), false);

			if (attributes.Length > 0)
				postRoute = ((PostAttribute)attributes[0]).Route;

			attributes = controllerType.GetCustomAttributes(typeof(PutAttribute), false);

			if (attributes.Length > 0)
				putRoute = ((PutAttribute)attributes[0]).Route;

			attributes = controllerType.GetCustomAttributes(typeof(DeleteAttribute), false);

			if (attributes.Length > 0)
				deleteRoute = ((DeleteAttribute)attributes[0]).Route;

			return !string.IsNullOrEmpty(getRoute)
			       || !string.IsNullOrEmpty(postRoute)
			       || !string.IsNullOrEmpty(putRoute)
			       || !string.IsNullOrEmpty(deleteRoute)
				? new ControllerRouteInfo(getRoute, postRoute, putRoute, deleteRoute)
				: null;
		}
	}
}