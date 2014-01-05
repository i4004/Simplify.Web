using AcspNet.Html;

using ApplicationHelper;

namespace AcspNet.Extensions
{
	/// <summary>
	/// Message display page state
	/// </summary>
	public interface IMessagePage : IHideObjectMembers
	{
		/// <summary>
		/// Gets or sets the message to be displayed on the message page.
		/// </summary>
		/// <value>
		/// The message to be displayed on the message page.
		/// </value>
		string Message { get; set; }

		/// <summary>
		/// Gets or sets the message status.
		/// </summary>
		/// <value>
		/// The message status.
		/// </value>
		MessageBoxStatus MessageStatus { get; set; }

		/// <summary>
		/// Navigates client to message page.
		/// </summary>
		void NavigateToMessagePage();

		/// <summary>
		/// Navigates client to message page.
		/// </summary>
		/// <param name="message">The message to be displayed on the message page.</param>
		/// <param name="status">The message status.</param>
		void NavigateToMessagePage(string message, MessageBoxStatus status = MessageBoxStatus.Information);

		/// <summary>
		/// Removes the message from the crurrent session.
		/// </summary>
		void RemoveMessage();
	}
}
