using System;

namespace AcspNet.Html
{
	/// <summary>
	/// MessageBox control
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
		public const string MessageBoxTemplatesPath = "AcspNet/MessageBox/";

		private readonly Manager _manager;

		internal MessageBox(Manager manager)
		{
			_manager = manager;
		}

		/// <summary>
		/// Generate message box HTML and set to data collector
		/// </summary>
		/// <param name="text">Text of message box</param>
		/// <param name="status">Status of the information</param>
		/// <param name="title">Title of message box</param>
		public void Show(string text, MessageBoxStatus status = MessageBoxStatus.Information, string title = "")
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

			var tpl = _manager.TemplateFactory.Load(templateFile);

			tpl.Set("Message", text);
			tpl.Set("Title", title == "" ? _manager.StringTable["FormTitleMessageBox"] : title);

			_manager.DataCollector.Add(_manager.Settings.MainContentVariableName, tpl.Get());
			_manager.DataCollector.Add(_manager.Settings.TitleVariableName, title == "" ? _manager.StringTable["FormTitleMessageBox"] : title);
		}

		/// <summary>
		///Generate message box HTML and set to data collector
		/// </summary>
		/// <param name="stringTableItemName">Show message from string table item</param>
		/// <param name="status">Status of the information</param>
		/// <param name="title">Title of message box</param>
		public void ShowSt(string stringTableItemName, MessageBoxStatus status = MessageBoxStatus.Information, string title = "")
		{
			Show(_manager.StringTable[stringTableItemName], status, title);
		}

		/// <summary>
		/// Get inline message box HTML
		/// </summary>
		/// <param name="text">Text of message box</param>
		/// <param name="status">Status of the information</param>
		/// <returns>Message box html</returns>
		public string GetInline(string text, MessageBoxStatus status = MessageBoxStatus.Information)
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

			var tpl = _manager.TemplateFactory.Load(templateFile);

			tpl.Set("Message", text);
			return tpl.Get();
		}

		/// <summary>
		/// Get inline message box HTML
		/// </summary>
		/// <param name="stringTableItemName">Show message from string table item</param>
		/// <param name="status">Status of the information</param>
		/// <returns>Message box html</returns>
		public string GetInlineSt(string stringTableItemName, MessageBoxStatus status = MessageBoxStatus.Information)
		{
			return GetInline(_manager.StringTable[stringTableItemName], status);
		}
	}

	/// <summary>
	/// The status of message box
	/// </summary>
	public enum MessageBoxStatus
	{
		/// <summary>
		/// The information status
		/// </summary>
		Information = 0,
		/// <summary>
		/// The error status
		/// </summary>
		Error = 1,
		/// <summary>
		/// The OK status
		/// </summary>
		Ok = 2
	}
}
