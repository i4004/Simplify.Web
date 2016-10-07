using System;

namespace Simplify.Web.Responses
{
	/// <summary>
	/// Provides template response (loads template and puts it to DataCollector)
	/// </summary>
	public class StaticTpl : ControllerResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Tpl" /> class.
		/// </summary>
		/// <param name="templateFileName">Name of the template file.</param>
		public StaticTpl(string templateFileName)
		{
			if (templateFileName == null)
				throw new ArgumentNullException(nameof(templateFileName));

			TemplateFileName = templateFileName;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tpl" /> class.
		/// </summary>
		/// <param name="templateFileName">Name of the template file.</param>
		/// <param name="title">The title.</param>
		/// <exception cref="ArgumentNullException">
		/// templateFileName
		/// or
		/// title
		/// </exception>
		public StaticTpl(string templateFileName, string title)
		{
			if (templateFileName == null)
				throw new ArgumentNullException(nameof(templateFileName));

			TemplateFileName = templateFileName;
			Title = title;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tpl" /> class.
		/// </summary>
		/// <param name="templateFileName">Name of the template file.</param>
		/// <param name="statusCode">The HTTP response status code.</param>
		/// <param name="title">The title.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public StaticTpl(string templateFileName, int statusCode, string title = null)
		{
			if (templateFileName == null)
				throw new ArgumentNullException(nameof(templateFileName));

			TemplateFileName = templateFileName;
			StatusCode = statusCode;
			Title = title;
		}

		/// <summary>
		/// Gets the name of the string table title item.
		/// </summary>
		/// <value>
		/// The name of the string table title item.
		/// </value>
		private string Title { get; }

		/// <summary>
		/// Gets the name of the template file.
		/// </summary>
		/// <value>
		/// The name of the template file.
		/// </value>
		public string TemplateFileName { get; }

		/// <summary>
		/// Gets the HTTP response status code.
		/// </summary>
		/// <value>
		/// The HTTP response status code.
		/// </value>
		public int? StatusCode { get; }

		/// <summary>
		/// Processes this response
		/// </summary>
		public override ControllerResponseResult Process()
		{
			if(StatusCode != null)
				Context.Response.StatusCode = StatusCode.Value;

			DataCollector.Add(TemplateFactory.Load(TemplateFileName));

			if (!string.IsNullOrEmpty(Title))
				DataCollector.AddTitle(Title);

			return ControllerResponseResult.Default;
		}
	}
}