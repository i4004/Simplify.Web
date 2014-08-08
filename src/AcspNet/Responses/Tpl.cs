using Simplify.Templates;

namespace AcspNet.Responses
{
	/// <summary>
	/// Template data result
	/// </summary>
	public class Tpl : ControllerResponse
	{
		private readonly string _data;
		private readonly string _title;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tpl"/> class.
		/// </summary>
		/// <param name="data">The data for main content variable.</param>
		public Tpl(string data)
		{
			_data = data;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tpl"/> class.
		/// </summary>
		/// <param name="data">The data for main content variable.</param>
		/// <param name="title">The site title.</param>
		public Tpl(string data, string title)
		{
			_data = data;
			_title = title;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tpl"/> class.
		/// </summary>
		/// <param name="template">The template.</param>
		public Tpl(ITemplate template)
		{
			if (template != null)
				_data = template.Get();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tpl" /> class.
		/// </summary>
		/// <param name="template">The template.</param>
		/// <param name="title">The site title.</param>
		public Tpl(ITemplate template, string title)
		{
			if (template != null)
				_data = template.Get();

			_title = title;
		}

		/// <summary>
		/// Gets the The data for main content variable.
		/// </summary>
		public string Data
		{
			get { return _data; }
		}

		/// <summary>
		/// Gets the site title.
		/// </summary>
		public string Title
		{
			get { return _title; }
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		public override void Process()
		{
			if (!string.IsNullOrEmpty(Data))
			{
				DataCollector.Add(Data);

				if (!string.IsNullOrEmpty(Title))
					DataCollector.AddTitle(Title);
			}
		}
	}
}