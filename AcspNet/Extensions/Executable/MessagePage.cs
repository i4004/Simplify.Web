using AcspNet.CoreExtensions.Library.Html;
using AcspNet.Extensions.Library;

namespace AcspNet.Extensions.Executable
{
	/// <summary>
	///  Message display page
	/// </summary>
	[Action("message")]
	[Version("1.0")]
	public sealed class MessagePage : ExecExtension
	{
		/// <summary>
		/// Invokes the executable extension.
		/// </summary>
		public override void Invoke()
		{
			var messagePageState = Manager.Get<MessagePageState>();

			if(string.IsNullOrEmpty(messagePageState.Message))
				Manager.Redirect("/");
			{
				var messageBox = Manager.Get<MessageBox>();

				messageBox.Show(messagePageState.Message, messagePageState.MessageStatus);

				messagePageState.Message = null;
			}
		}
	}
}

