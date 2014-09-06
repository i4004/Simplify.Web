using Simplify.Templates;

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
		/// <param name="stringTableTitleItemName">Name of the string table title item.</param>
		public ViewModel(string templateFileName, T viewModel = null, string stringTableTitleItemName = null)
		{
			TemplateFileName = templateFileName;
			Model = viewModel;
			StringTableTitleItemName = stringTableTitleItemName;
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
		/// Gets or sets the name of the string table title item.
		/// </summary>
		/// <value>
		/// The name of the string table title item.
		/// </value>
		public string StringTableTitleItemName { get; set; }

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		public override ControllerResponseResult Process()
		{
			DataCollector.Add(TemplateFactory.Load(TemplateFileName).Model(Model).Set());

			if (!string.IsNullOrEmpty(StringTableTitleItemName))
				DataCollector.AddTitle(StringTableManager.GetItem(StringTableTitleItemName));

			return ControllerResponseResult.Default;
		}
	}
}