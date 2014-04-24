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

		internal MessagePage(INavigator navigator)
		{
			_navigator = navigator;
		}

		///// <summary>
		///// Gets or sets the message to be displayed on the message page.
		///// </summary>
		///// <value>
		///// The message to be displayed on the message page.
		///// </value>
		//public string Message
		//{
		//	get
		//	{
		//		return (string)Manager.Session[MessageSessionFieldName];
		//	}
		//	set
		//	{
		//		Manager.Session.Add(MessageSessionFieldName, value);
		//	}
		//}

		///// <summary>
		///// Gets or sets the message status.
		///// </summary>
		///// <value>
		///// The message status.
		///// </value>
		//public MessageBoxStatus MessageStatus
		//{
		//	get
		//	{
		//		if (Manager.Session[MessageStatusSessionFieldName] != null)
		//			return (MessageBoxStatus)Manager.Session[MessageStatusSessionFieldName];

		//		return MessageBoxStatus.Information;
		//	}
		//	set
		//	{
		//		Manager.Session.Add(MessageStatusSessionFieldName, value);
		//	}
		//}

		///// <summary>
		///// Navigates client to message page.
		///// </summary>
		//public void NavigateToMessagePage()
		//{
		//	Manager.Redirect("message");
		//}

		///// <summary>
		///// Navigates client to message page.
		///// </summary>
		///// <param name="message">The message to be displayed on the message page.</param>
		///// <param name="status">The message status.</param>
		//public void NavigateToMessagePage(string message, MessageBoxStatus status = MessageBoxStatus.Error)
		//{
		//	Message = message;
		//	MessageStatus = status;

		//	NavigateToMessagePage();
		//}

		///// <summary>
		///// Removes the message from the crurrent session.
		///// </summary>
		//public void RemoveMessage()
		//{
		//	Manager.Session.Remove(MessageSessionFieldName);
		//}
	}
}