using System.Web;
using AcspNet.Html;

namespace AcspNet
{
	/// <summary>
	/// Message display page state
	/// </summary>
	public sealed class MessagePage : IMessagePage
	{
		private const string MessageSessionFieldName = "Message";
		private const string MessageStatusSessionFieldName = "MessageStatus";

		private readonly INavigator _navigator;
		private readonly HttpSessionStateBase _session;

		internal MessagePage(INavigator navigator, HttpSessionStateBase session)
		{
			_navigator = navigator;
			_session = session;
		}

		/// <summary>
		/// Gets or sets the message to be displayed on the message page.
		/// </summary>
		/// <value>
		/// The message to be displayed on the message page.
		/// </value>
		public string Message
		{
			get
			{
				return (string)_session[MessageSessionFieldName];
			}
			set
			{
				_session.Add(MessageSessionFieldName, value);
			}
		}

		/// <summary>
		/// Gets or sets the message status.
		/// </summary>
		/// <value>
		/// The message status.
		/// </value>
		public MessageBoxStatus MessageStatus
		{
			get
			{
				if (_session[MessageStatusSessionFieldName] != null)
					return (MessageBoxStatus)_session[MessageStatusSessionFieldName];

				return MessageBoxStatus.Information;
			}
			set
			{
				_session.Add(MessageStatusSessionFieldName, value);
			}
		}

		/// <summary>
		/// Navigates client to message page.
		/// </summary>
		public void NavigateToMessagePage()
		{
			_navigator.Redirect("message");
		}

		/// <summary>
		/// Navigates client to message page.
		/// </summary>
		/// <param name="message">The message to be displayed on the message page.</param>
		/// <param name="status">The message status.</param>
		public void NavigateToMessagePage(string message, MessageBoxStatus status = MessageBoxStatus.Error)
		{
			Message = message;
			MessageStatus = status;

			NavigateToMessagePage();
		}

		/// <summary>
		/// Removes the message from the crurrent session.
		/// </summary>
		public void RemoveMessage()
		{
			_session.Remove(MessageSessionFieldName);
		}
	}
}