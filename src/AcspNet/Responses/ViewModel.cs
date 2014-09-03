using Simplify.Templates;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides view model response
	/// </summary>
	public class ViewModel<T> : ControllerResponse
		where T : class 
	{
		private readonly string _templateFileName;
		private readonly T _viewModel;

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModel{T}" /> class.
		/// </summary>
		/// <param name="templateFileName">Name of the template file.</param>
		/// <param name="viewModel">The view model.</param>
		public ViewModel(string templateFileName, T viewModel = null)
		{
			_templateFileName = templateFileName;
			_viewModel = viewModel;
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		public override ControllerResponseResult Process()
		{
			DataCollector.Add(TemplateFactory.Load(_templateFileName).Model(_viewModel).Set());

			return ControllerResponseResult.Default;
		}
	}
}