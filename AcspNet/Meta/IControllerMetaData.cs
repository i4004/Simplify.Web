using System;

namespace AcspNet.Meta
{
	public interface IControllerMetaData
	{
		/// <summary>
		/// Gets the type of the controller.
		/// </summary>
		/// <value>
		/// The type of the extension.
		/// </value>
		Type ControllerType { get; }

		/// <summary>
		/// Gets the controller execute parameters.
		/// </summary>
		ControllerExecParameters ExecParameters { get; }
	}
}