namespace AcspNet.Modules.Controllers
{
	/// <summary>
	/// Message display page
	/// </summary>
	[Action("message")]
	public class MessagePageDisplay : Controller
	{
		/// <summary>
		/// Invokes the controller.
		/// </summary>
		public override void Invoke()
		{
			if (string.IsNullOrEmpty(MessagePage.Message))
				Navigator.NavigateToDefaultPage();
			else
			{
				MessageBox.Show(MessagePage.Message, MessagePage.MessageStatus);
				MessagePage.RemoveMessage();
			}
		}
	}
}