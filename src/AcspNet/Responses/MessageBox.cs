using System;
using AcspNet.Modules.Html;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides message box response (generate message box and puts it to data collector)
	/// </summary>
	public class MessageBox : ControllerResponse
	{
		private readonly string _text;
		private readonly MessageBoxStatus _status;
		private readonly string _messageBoxTitle;

		/// <summary>
		/// Initializes a new instance of the <see cref="MessageBox"/> class.
		/// </summary>
		/// <param name="text">The message box text.</param>
		/// <param name="status">The message box status.</param>
		/// <param name="messageBoxTitle">The message box title.</param>
		public MessageBox(string text, MessageBoxStatus status = MessageBoxStatus.Error, string messageBoxTitle = null)
		{
			_text = text;
			_status = status;
			_messageBoxTitle = messageBoxTitle;
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <returns></returns>
		public override ControllerResponseResult Process()
		{
			Html.MessageBox.Show(_text, _status, _messageBoxTitle);

			return ControllerResponseResult.Default;
		}
	}
}