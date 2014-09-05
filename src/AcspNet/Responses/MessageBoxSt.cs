using AcspNet.Modules.Html;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides message box response with data from string table (generate message box and puts it to data collector)
	/// </summary>
	public class MessageBoxSt : ControllerResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageBox"/> class.
		/// </summary>
		/// <param name="stringTableItemName">The string table item name.</param>
		/// <param name="status">The message box status.</param>
		/// <param name="messageBoxTitle">The message box title.</param>
		public MessageBoxSt(string stringTableItemName, MessageBoxStatus status = MessageBoxStatus.Error, string messageBoxTitle = null)
		{
			StringTableItemName = stringTableItemName;
			Status = status;
			MessageBoxTitle = messageBoxTitle;
		}

		/// <summary>
		/// Gets the name of the string table item.
		/// </summary>
		/// <value>
		/// The name of the string table item.
		/// </value>
		public string StringTableItemName { get; private set; }

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
			Html.MessageBox.ShowSt(StringTableItemName, Status, MessageBoxTitle);

			return ControllerResponseResult.Default;
		}
	}
}