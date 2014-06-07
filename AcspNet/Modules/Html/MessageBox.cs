using System;

namespace AcspNet.Modules.Html
{
	/// <summary>
	/// The HTML message box
	/// Usable template files:
	/// "AcspNet/MessageBox/InfoMessageBox.tpl"
	/// "AcspNet/MessageBox/ErrorMessageBox.tpl"
	/// "AcspNet/MessageBox/OkMessageBox.tpl"
	/// "AcspNet/MessageBox/InlineInfoMessageBox.tpl"
	/// "AcspNet/MessageBox/InlineErrorMessageBox.tpl"
	/// "AcspNet/MessageBox/InlineOkMessageBox.tpl"
	/// Usable <see cref="StringTable"/> items:
	/// "FormTitleMessageBox"
	/// Template variables:
	/// "Message"
	/// "Title"
	/// </summary>
	public sealed class MessageBox : IMessageBox
	{
		private const string MessageBoxTemplatesPath = "AcspNet/MessageBox/";

		private readonly ITemplateFactory _templateFactory;
		private readonly IStringTable _stringTable;
		private readonly IDataCollector _dataCollector;

		internal MessageBox(ITemplateFactory templateFactory, IStringTable stringTable, IDataCollector dataCollector)
		{
			_templateFactory = templateFactory;
			_stringTable = stringTable;
			_dataCollector = dataCollector;
		}

		/// <summary>
		/// Generate message box HTML and set to data collector
		/// </summary>
		/// <param name="text">Text of message box</param>
		/// <param name="status">Status of the information</param>
		/// <param name="title">Title of message box</param>
		public void Show(string text, MessageBoxStatus status = MessageBoxStatus.Error, string title = "")
		{
			if(string.IsNullOrEmpty(text))
				throw new ArgumentNullException("text");

			if(title == null)
				throw new ArgumentNullException("title");

			var templateFile = MessageBoxTemplatesPath;

			switch (status)
			{
				case MessageBoxStatus.Information:
					templateFile += "InfoMessageBox.tpl";
					break;
				case MessageBoxStatus.Error:
					templateFile += "ErrorMessageBox.tpl";
					break;
				case MessageBoxStatus.Ok:
					templateFile += "OkMessageBox.tpl";
					break;
			}

			var tpl = _templateFactory.Load(templateFile);

			tpl.Set("Message", text);
			tpl.Set("Title", title == "" ? _stringTable["FormTitleMessageBox"] : title);

			_dataCollector.Add(tpl.Get());
			_dataCollector.AddTitle(title == "" ? _stringTable["FormTitleMessageBox"] : title);
		}

		/// <summary>
		///Generate message box HTML and set to data collector
		/// </summary>
		/// <param name="stringTableItemName">Show message from string table item</param>
		/// <param name="status">Status of the information</param>
		/// <param name="title">Title of message box</param>
		public void ShowSt(string stringTableItemName, MessageBoxStatus status = MessageBoxStatus.Error, string title = "")
		{
			Show(_stringTable[stringTableItemName], status, title);
		}

		/// <summary>
		/// Get inline message box HTML
		/// </summary>
		/// <param name="text">Text of message box</param>
		/// <param name="status">Status of the information</param>
		/// <returns>Message box html</returns>
		public string GetInline(string text, MessageBoxStatus status = MessageBoxStatus.Error)
		{
			if (string.IsNullOrEmpty(text))
				throw new ArgumentNullException("text");

			var templateFile = MessageBoxTemplatesPath;

			switch (status)
			{
				case MessageBoxStatus.Information:
					templateFile += "InlineInfoMessageBox.tpl";
					break;
				case MessageBoxStatus.Error:
					templateFile += "InlineErrorMessageBox.tpl";
					break;
				case MessageBoxStatus.Ok:
					templateFile += "InlineOkMessageBox.tpl";
					break;
			}

			var tpl = _templateFactory.Load(templateFile);

			tpl.Set("Message", text);
			return tpl.Get();
		}

		/// <summary>
		/// Get inline message box HTML
		/// </summary>
		/// <param name="stringTableItemName">Show message from string table item</param>
		/// <param name="status">Status of the information</param>
		/// <returns>Message box html</returns>
		public string GetInlineSt(string stringTableItemName, MessageBoxStatus status = MessageBoxStatus.Error)
		{
			return GetInline(_stringTable[stringTableItemName], status);
		}
	}
}
