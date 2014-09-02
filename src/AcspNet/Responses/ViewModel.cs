using System;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides view model response
	/// </summary>
	public class ViewModel<T> : ControllerResponse
		where T : class 
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModel{T}" /> class.
		/// </summary>
		/// <param name="templateFileName">Name of the template file.</param>
		/// <param name="viewModel">The view model.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public ViewModel(string templateFileName, T viewModel = null)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		public override ControllerResponseResult Process()
		{
			throw new NotImplementedException();
		}
	}
}