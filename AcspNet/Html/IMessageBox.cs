using ApplicationHelper;

namespace AcspNet.Html
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
	public interface IMessageBox : IHideObjectMembers
	{
		/// <summary>
		/// Generate message box HTML and set to data collector
		/// </summary>
		/// <param name="text">Text of message box</param>
		/// <param name="status">Status of the information</param>
		/// <param name="title">Title of message box</param>
		void Show(string text, MessageBoxStatus status = MessageBoxStatus.Information, string title = "");

		/// <summary>
		///Generate message box HTML and set to data collector
		/// </summary>
		/// <param name="stringTableItemName">Show message from string table item</param>
		/// <param name="status">Status of the information</param>
		/// <param name="title">Title of message box</param>
		void ShowSt(string stringTableItemName, MessageBoxStatus status = MessageBoxStatus.Information, string title = "");

		/// <summary>
		/// Get inline message box HTML
		/// </summary>
		/// <param name="text">Text of message box</param>
		/// <param name="status">Status of the information</param>
		/// <returns>Message box html</returns>
		string GetInline(string text, MessageBoxStatus status = MessageBoxStatus.Information);

		/// <summary>
		/// Get inline message box HTML
		/// </summary>
		/// <param name="stringTableItemName">Show message from string table item</param>
		/// <param name="status">Status of the information</param>
		/// <returns>Message box html</returns>
		string GetInlineSt(string stringTableItemName, MessageBoxStatus status = MessageBoxStatus.Information);
	}
}