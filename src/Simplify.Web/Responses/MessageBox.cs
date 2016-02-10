using Simplify.Web.Modules.Html;

namespace Simplify.Web.Responses
{
	/// <summary>
	/// Provides message box response (generate message box and puts it to data collector)
	/// </summary>
	public class MessageBox : ControllerResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageBox" /> class.
		/// </summary>
		/// <param name="text">The message box text.</param>
		/// <param name="status">The message box status.</param>
		/// <param name="title">The title.</param>
		public MessageBox(string text, MessageBoxStatus status = MessageBoxStatus.Error, string title = null)
		{
			Text = text;
			Status = status;
			Title = title;
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
		/// Gets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get; }

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <returns></returns>
		public override ControllerResponseResult Process()
		{
			Html.MessageBox.Show(Text, Status, Title);

			return ControllerResponseResult.Default;
		}
	}
}