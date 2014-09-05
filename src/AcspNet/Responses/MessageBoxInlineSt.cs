using AcspNet.Modules.Html;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides inline message box response (generate inline message box and sends it to user only, without site generation)
	/// </summary>
	public class MessageBoxInlineSt : ControllerResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageBox" /> class.
		/// </summary>
		/// <param name="stringTableItemName">Name of the string table item.</param>
		/// <param name="status">The message box status.</param>
		public MessageBoxInlineSt(string stringTableItemName, MessageBoxStatus status = MessageBoxStatus.Error)
		{
			StringTableItemName = stringTableItemName;
			Status = status;
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
		/// Processes this response
		/// </summary>
		/// <returns></returns>
		public override ControllerResponseResult Process()
		{
			ResponseWriter.Write(Html.MessageBox.GetInlineSt(StringTableItemName, Status), Context.Response);

			return ControllerResponseResult.RawOutput;
		}
	}
}