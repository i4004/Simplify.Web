using AcspNet.Modules.Html;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides inline message box response (generate inline message box and sends it to user only, without site generation)
	/// </summary>
	public class MessageBoxInlineSt : ControllerResponse
	{
		private readonly string _stringTableItemName;
		private readonly MessageBoxStatus _status;

		/// <summary>
		/// Initializes a new instance of the <see cref="MessageBox" /> class.
		/// </summary>
		/// <param name="stringTableItemName">Name of the string table item.</param>
		/// <param name="status">The message box status.</param>
		public MessageBoxInlineSt(string stringTableItemName, MessageBoxStatus status = MessageBoxStatus.Error)
		{
			_stringTableItemName = stringTableItemName;
			_status = status;
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <returns></returns>
		public override ControllerResponseResult Process()
		{
			ResponseWriter.Write(Html.MessageBox.GetInlineSt(_stringTableItemName, _status), Context.Response);

			return ControllerResponseResult.RawOutput;
		}
	}
}