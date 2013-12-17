namespace AcspNet.Extensions.Library
{
	/// <summary>
	/// Message display page state
	/// </summary>
	[Priority(-5)]
	[Version("1.0")]
	public sealed class MessagePageState : ILibExtension
	{
		private const string MessageSessionFieldName = "Message";
		private const string MessageStatusSessionFieldName = "MessageStatus";

		private Manager _manager;

		/// <summary>
		/// Initializes the library extension.
		/// </summary>
		/// <param name="manager">The manager.</param>
		public void Initialize(Manager manager)
		{
			_manager = manager;
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
				return (string)_manager.Session[MessageSessionFieldName];
			}
			set
			{
				_manager.Session.Add(MessageSessionFieldName, value);
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
				if (_manager.Session[MessageStatusSessionFieldName] != null)
					return (MessageBoxStatus)_manager.Session[MessageStatusSessionFieldName];

				return MessageBoxStatus.Information;
			}
			set
			{
				_manager.Session.Add(MessageStatusSessionFieldName, value);
			}
		}
		
		/// <summary>
		/// Navigates client to message page.
		/// </summary>
		public void NavigateToMessagePage()
		{
			_manager.Redirect("message");
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
	}
}
