using System;
using AcspNet.Modules.Html;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides inline message box response (generate inline message box and sends it to user only, without site generation)
	/// </summary>
	public class MessageBoxInline : ControllerResponse
	{
		private readonly string _text;
		private readonly MessageBoxStatus _status;

		/// <summary>
		/// Initializes a new instance of the <see cref="MessageBox"/> class.
		/// </summary>
		/// <param name="text">The message box text.</param>
		/// <param name="status">The message box status.</param>
		public MessageBoxInline(string text, MessageBoxStatus status = MessageBoxStatus.Error)
		{
			_text = text;
			_status = status;
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <returns></returns>
		public override ControllerResponseResult Process()
		{
			ResponseWriter.Write(Html.MessageBox.GetInline(_text, _status), Context.Response);

			return ControllerResponseResult.RawOutput;
		}
	}
}