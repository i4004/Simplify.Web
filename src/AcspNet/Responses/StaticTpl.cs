using System;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides template response (loads template and puts it to DataCollector)
	/// </summary>
	public class StaticTpl : ControllerResponse
	{
		private readonly string _stringTableTitleItemName;
		private readonly string _templateFileName;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tpl" /> class.
		/// </summary>
		/// <param name="templateFileName">Name of the template file.</param>
		public StaticTpl(string templateFileName)
		{
			if (templateFileName == null) throw new ArgumentNullException("templateFileName");

			_templateFileName = templateFileName;
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

			_templateFileName = templateFileName;
			_stringTableTitleItemName = stringTableTitleItemName;
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		public override ControllerResponseResult Process()
		{
			DataCollector.Add(TemplateFactory.Load(_templateFileName));

			if (!string.IsNullOrEmpty(_stringTableTitleItemName))
				DataCollector.AddTitle(StringTableManager.GetItem(_stringTableTitleItemName));

			return ControllerResponseResult.Default;
		}
	}
}