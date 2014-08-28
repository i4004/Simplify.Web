using AcspNet.Modules.Html;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides message box response with data from string table (generate message box and puts it to data collector)
	/// </summary>
	public class MessageBoxSt : ControllerResponse
	{
		private readonly string _stringTableItemName;
		private readonly MessageBoxStatus _status;
		private readonly string _messageBoxTitle;

		/// <summary>
		/// Initializes a new instance of the <see cref="MessageBox"/> class.
		/// </summary>
		/// <param name="stringTableItemName">The string table item name.</param>
		/// <param name="status">The message box status.</param>
		/// <param name="messageBoxTitle">The message box title.</param>
		public MessageBoxSt(string stringTableItemName, MessageBoxStatus status = MessageBoxStatus.Error, string messageBoxTitle = null)
		{
			_stringTableItemName = stringTableItemName;
			_status = status;
			_messageBoxTitle = messageBoxTitle;
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <returns></returns>
		public override ControllerResponseResult Process()
		{
			Html.MessageBox.ShowSt(_stringTableItemName, _status, _messageBoxTitle);

			return ControllerResponseResult.Default;
		}
	}
}