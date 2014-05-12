using System;

namespace AcspNet.Meta
{
	/// <summary>
	/// Controller meta-data information container
	/// </summary>
	public class ControllerMetaContainer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerMetaContainer" /> class.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <param name="execParameters">The execution parameters.</param>
		/// <param name="security">The controller security.</param>
		/// <param name="role">The controller role.</param>
		public ControllerMetaContainer(Type controllerType, ControllerExecParameters execParameters = null, ControllerSecurity security = null, ControllerRole role = null)
		{
			ControllerType = controllerType;
			ExecParameters = execParameters;
			Security = security;
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
		/// Gets the controller security information.
		/// </summary>
		public ControllerSecurity Security { get; private set; }

		/// <summary>
		/// Gets the controller role information.
		/// </summary>
		public ControllerRole Role { get; private set; }
	}
}