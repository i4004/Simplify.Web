using System;

namespace Simplify.Web.Modules.Html
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

		/// <summary>
		/// Initializes a new instance of the <see cref="MessageBox"/> class.
		/// </summary>
		/// <param name="templateFactory">The template factory.</param>
		/// <param name="stringTable">The string table.</param>
		/// <param name="dataCollector">The data collector.</param>
		public MessageBox(ITemplateFactory templateFactory, IStringTable stringTable, IDataCollector dataCollector)
		{
			_templateFactory = templateFactory;
			_stringTable = stringTable;
			_dataCollector = dataCollector;
		}

		/// <summary>
		/// Generate message box HTML and set to data collector
		/// </summary>
		/// <param name="text">Text of a message box</param>
		/// <param name="status">Status of a message box</param>
		/// <param name="title">Title of a message box</param>
		public void Show(string text, MessageBoxStatus status = MessageBoxStatus.Error, string title = null)
		{
			if (string.IsNullOrEmpty(text))
				throw new ArgumentNullException(nameof(text));

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
			tpl.Set("Title", string.IsNullOrEmpty(title) ? _stringTable.GetItem("FormTitleMessageBox") : title);

			_dataCollector.Add(tpl.Get());
			_dataCollector.AddTitle(string.IsNullOrEmpty(title) ? _stringTable.GetItem("FormTitleMessageBox") : title);
		}

		/// <summary>
		///Generate message box HTML and set to data collector
		/// </summary>
		/// <param name="stringTableItemName">Show message from string table item</param>
		/// <param name="status">Status of a message box</param>
		/// <param name="title">Title of a message box</param>
		public void ShowSt(string stringTableItemName, MessageBoxStatus status = MessageBoxStatus.Error, string title = null)
		{
			Show(_stringTable.GetItem(stringTableItemName), status, title);
		}

		/// <summary>
		/// Get inline message box HTML
		/// </summary>
		/// <param name="text">Text of a message box</param>
		/// <param name="status">Status of a message box</param>
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
		/// <param name="status">Status of a message box</param>
		/// <returns>Message box html</returns>
		public string GetInlineSt(string stringTableItemName, MessageBoxStatus status = MessageBoxStatus.Error)
		{
			return GetInline(_stringTable.GetItem(stringTableItemName), status);
		}
	}
}
