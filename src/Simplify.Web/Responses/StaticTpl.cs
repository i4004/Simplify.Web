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
			if (templateFileName == null) throw new ArgumentNullException(nameof(templateFileName));

			TemplateFileName = templateFileName;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tpl" /> class.
		/// </summary>
		/// <param name="templateFileName">Name of the template file.</param>
		/// <param name="title">The title.</param>
		/// <exception cref="System.ArgumentNullException">
		/// templateFileName
		/// or
		/// title
		/// </exception>
		public StaticTpl(string templateFileName, string title)
		{
			if (templateFileName == null) throw new ArgumentNullException(nameof(templateFileName));
			if (title == null) throw new ArgumentNullException(nameof(title));

			TemplateFileName = templateFileName;
			Title = title;
		}

		/// <summary>
		/// Gets the name of the string table title item.
		/// </summary>
		/// <value>
		/// The name of the string table title item.
		/// </value>
		public string Title { get; }

		/// <summary>
		/// Gets the name of the template file.
		/// </summary>
		/// <value>
		/// The name of the template file.
		/// </value>
		public string TemplateFileName { get; }

		/// <summary>
		/// Processes this response
		/// </summary>
		public override ControllerResponseResult Process()
		{
			DataCollector.Add(TemplateFactory.Load(TemplateFileName));

			if (!string.IsNullOrEmpty(Title))
				DataCollector.AddTitle(Title);

			return ControllerResponseResult.Default;
		}
	}
}