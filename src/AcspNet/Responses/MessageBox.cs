using AcspNet.Modules.Html;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides message box response (generate message box and puts it to data collector)
	/// </summary>
	public class MessageBox : ControllerResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageBox"/> class.
		/// </summary>
		/// <param name="text">The message box text.</param>
		/// <param name="status">The message box status.</param>
		/// <param name="messageBoxTitle">The message box title.</param>
		public MessageBox(string text, MessageBoxStatus status = MessageBoxStatus.Error, string messageBoxTitle = null)
		{
			Text = text;
			Status = status;
			MessageBoxTitle = messageBoxTitle;
		}

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public string Text { get; private set; }

		/// <summary>
		/// Gets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		public MessageBoxStatus Status { get; private set; }

		/// <summary>
		/// Gets the message box title.
		/// </summary>
		/// <value>
		/// The message box title.
		/// </value>
		public string MessageBoxTitle { get; private set; }

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <returns></returns>
		public override ControllerResponseResult Process()
		{
			Html.MessageBox.Show(Text, Status, MessageBoxTitle);

			return ControllerResponseResult.Default;
		}
	}
}