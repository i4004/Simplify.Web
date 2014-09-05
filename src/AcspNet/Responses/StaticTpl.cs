using System;

namespace AcspNet.Responses
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
			if (templateFileName == null) throw new ArgumentNullException("templateFileName");

			TemplateFileName = templateFileName;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tpl" /> class.
		/// </summary>
		/// <param name="templateFileName">Name of the template file.</param>
		/// <param name="stringTableTitleItemName">Name of the string table title item.</param>
		public StaticTpl(string templateFileName, string stringTableTitleItemName)
		{
			if (templateFileName == null) throw new ArgumentNullException("templateFileName");
			if (stringTableTitleItemName == null) throw new ArgumentNullException("stringTableTitleItemName");

			TemplateFileName = templateFileName;
			StringTableTitleItemName = stringTableTitleItemName;
		}

		/// <summary>
		/// Gets the name of the string table title item.
		/// </summary>
		/// <value>
		/// The name of the string table title item.
		/// </value>
		public string StringTableTitleItemName { get; private set; }

		/// <summary>
		/// Gets the name of the template file.
		/// </summary>
		/// <value>
		/// The name of the template file.
		/// </value>
		public string TemplateFileName { get; private set; }

		/// <summary>
		/// Processes this response
		/// </summary>
		public override ControllerResponseResult Process()
		{
			DataCollector.Add(TemplateFactory.Load(TemplateFileName));

			if (!string.IsNullOrEmpty(StringTableTitleItemName))
				DataCollector.AddTitle(StringTableManager.GetItem(StringTableTitleItemName));

			return ControllerResponseResult.Default;
		}
	}
}