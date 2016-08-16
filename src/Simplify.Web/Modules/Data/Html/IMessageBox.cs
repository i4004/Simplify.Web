namespace Simplify.Web.Modules.Data.Html
{
	/// <summary>
	/// The HTML message box
	/// Usable template files:
	/// "Simplify.Web/MessageBox/InfoMessageBox.tpl"
	/// "Simplify.Web/MessageBox/ErrorMessageBox.tpl"
	/// "Simplify.Web/MessageBox/OkMessageBox.tpl"
	/// "Simplify.Web/MessageBox/InlineInfoMessageBox.tpl"
	/// "Simplify.Web/MessageBox/InlineErrorMessageBox.tpl"
	/// "Simplify.Web/MessageBox/InlineOkMessageBox.tpl"
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
		/// <param name="text">Text of a message box</param>
		/// <param name="status">Status of a message box</param>
		/// <param name="title">Title of a message box</param>
		void Show(string text, MessageBoxStatus status = MessageBoxStatus.Error, string title = null);

		/// <summary>
		///Generate message box HTML and set to data collector
		/// </summary>
		/// <param name="stringTableItemName">Show message from string table item</param>
		/// <param name="status">Status of a message box</param>
		/// <param name="title">Title of a message box</param>
		void ShowSt(string stringTableItemName, MessageBoxStatus status = MessageBoxStatus.Error, string title = null);

		/// <summary>
		/// Get inline message box HTML
		/// </summary>
		/// <param name="text">Text of a message box</param>
		/// <param name="status">Status of a message box</param>
		/// <returns>Message box html</returns>
		string GetInline(string text, MessageBoxStatus status = MessageBoxStatus.Error);

		/// <summary>
		/// Get inline message box HTML
		/// </summary>
		/// <param name="stringTableItemName">Show message from string table item</param>
		/// <param name="status">Status of a message box</param>
		/// <returns>Message box html</returns>
		string GetInlineSt(string stringTableItemName, MessageBoxStatus status = MessageBoxStatus.Error);
	}
}