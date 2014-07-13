using System;

namespace AcspNet.Meta
{
	/// <summary>
	/// Controller meta-data information
	/// </summary>
	public class ControllerMetaData : IControllerMetaData
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerMetaData" /> class.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <param name="execParameters">The execute parameters.</param>
		/// <param name="role">The controller role.</param>
		public ControllerMetaData(Type controllerType, ControllerExecParameters execParameters = null, ControllerRole role = null)
		{
			ControllerType = controllerType;
			ExecParameters = execParameters;
			Role = role;
		}

		/// <summary>
		/// Gets the type of the controller.
		/// </summary>
		/// <value>
		/// The type of the extension.
		/// </value>
		public Type ControllerType { get; private set; }

		/// <summary>
		/// Gets the controller execute parameters.
		/// </summary>
		public ControllerExecParameters ExecParameters { get; private set; }

		/// <summary>
		/// Gets the controller role information.
		/// </summary>
		public ControllerRole Role { get; private set; }
	}
}