using System;

namespace AcspNet.Meta
{
	/// <summary>
	/// Controller data meta information
	/// </summary>
	public class ControllerDataParameters
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerRole" /> class.
		/// </summary>
		/// <param name="viewModel">The controller view model.</param>
		public ControllerDataParameters(Type viewModel)
		{
			ViewModel = viewModel;
		}

		/// <summary>
		/// Gets the controller view model.
		/// </summary>
		/// <value>
		/// The controller view model.
		/// </value>
		public Type ViewModel { get; private set; }
	}
}