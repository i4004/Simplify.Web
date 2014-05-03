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

		public ControllerExecParameters ExecParameters { get; set; }

		public ControllerSecurity Security { get; set; }
		public ControllerRole Role { get; set; }
	}
}