using Simplify.Templates;

namespace Simplify.Web.Responses
{
	/// <summary>
	/// Provides template response (puts data to DataCollector)
	/// </summary>
	public class Tpl : ControllerResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Tpl"/> class.
		/// </summary>
		/// <param name="data">The data for main content variable.</param>
		public Tpl(string data)
		{
			Data = data;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tpl"/> class.
		/// </summary>
		/// <param name="data">The data for main content variable.</param>
		/// <param name="title">The site title.</param>
		public Tpl(string data, string title)
		{
			Data = data;
			Title = title;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tpl"/> class.
		/// </summary>
		/// <param name="template">The template.</param>
		public Tpl(ITemplate template)
		{
			if (template != null)
				Data = template.Get();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tpl" /> class.
		/// </summary>
		/// <param name="template">The template.</param>
		/// <param name="title">The site title.</param>
		public Tpl(ITemplate template, string title)
		{
			if (template != null)
				Data = template.Get();

			Title = title;
		}

		/// <summary>
		/// Gets the The data for main content variable.
		/// </summary>
		public string Data { get; }

		/// <summary>
		/// Gets the site title.
		/// </summary>
		public string Title { get; }

		/// <summary>
		/// Processes this response
		/// </summary>
		public override ControllerResponseResult Process()
		{
			DataCollector.Add(Data);

			if (!string.IsNullOrEmpty(Title))
				DataCollector.AddTitle(Title);

			return ControllerResponseResult.Default;
		}
	}
}