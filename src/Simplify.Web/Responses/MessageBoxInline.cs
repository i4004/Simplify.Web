using Simplify.Web.Modules.Data.Html;

namespace Simplify.Web.Responses
{
	/// <summary>
	/// Provides inline message box response (generate inline message box and sends it to user only, without site generation)
	/// </summary>
	public class MessageBoxInline : ControllerResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageBox"/> class.
		/// </summary>
		/// <param name="text">The message box text.</param>
		/// <param name="status">The message box status.</param>
		public MessageBoxInline(string text, MessageBoxStatus status = MessageBoxStatus.Error)
		{
			Text = text;
			Status = status;
		}

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public string Text { get; }

		/// <summary>
		/// Gets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		public MessageBoxStatus Status { get; }

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <returns></returns>
		public override ControllerResponseResult Process()
		{
			ResponseWriter.Write(Html.MessageBox.GetInline(Text, Status), Context.Response);

			return ControllerResponseResult.RawOutput;
		}
	}
}