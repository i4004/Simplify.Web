using System;

namespace AcspNet.Meta
{
	/// <summary>
	/// Controller meta-data information
	/// </summary>
	public class ControllerMetaData
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerMetaData" /> class.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		public ControllerMetaData(Type controllerType)
		{
			ControllerType = controllerType;
		}

		/// <summary>
		/// Gets the type of the controller.
		/// </summary>
		/// <value>
		/// The type of the extension.
		/// </value>
		public Type ControllerType { get; private set; }
	}
}