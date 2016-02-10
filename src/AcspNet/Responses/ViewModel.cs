namespace Simplify.Web.Responses
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
		/// <param name="title">The title.</param>
		public ViewModel(string templateFileName, T viewModel = null, string title = null)
		{
			TemplateFileName = templateFileName;
			Model = viewModel;
			Title = title;
		}

		/// <summary>
		/// Gets the name of the template file.
		/// </summary>
		/// <value>
		/// The name of the template file.
		/// </value>
		public string TemplateFileName { get; private set; }

		/// <summary>
		/// Gets the model.
		/// </summary>
		/// <value>
		/// The model.
		/// </value>
		public T Model { get; private set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get; set; }

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		public override ControllerResponseResult Process()
		{
			DataCollector.Add(TemplateFactory.Load(TemplateFileName).Model(Model).Set());

			if (!string.IsNullOrEmpty(Title))
				DataCollector.AddTitle(Title);

			return ControllerResponseResult.Default;
		}
	}
}