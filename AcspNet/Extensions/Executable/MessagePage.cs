using AcspNet.Extensions.Library;

namespace AcspNet.Extensions.Executable
{
	/// <summary>
	///  Message display page
	/// </summary>
	[Action("message")]
	[Version("1.0")]
	public sealed class MessagePage : IExecExtension
	{
		/// <summary>
		/// Invokes the executable extension.
		/// </summary>
		/// <param name="manager">The manager.</param>
		public void Invoke(Manager manager)
		{
			var messagePageState = manager.Get<MessagePageState>();

			if(string.IsNullOrEmpty(messagePageState.Message))
				manager.Redirect("/");
			{
				var messageBox = manager.Get<MessageBox>();

				messageBox.Show(messagePageState.Message, messagePageState.MessageStatus);

				messagePageState.Message = null;
			}
		}
	}
}

