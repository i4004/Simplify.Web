namespace AcspNet.Modules.Controllers
{
	/// <summary>
	/// Message display page
	/// </summary>
	[Action("message")]
	public sealed class MessagePageDisplay : Controller
	{
		public override void Invoke()
		{
			//if (string.IsNullOrEmpty(Extensions.MessagePage.Message))
			//	Manager.Redirect(AcspNet.Manager.SiteVirtualPath + "/");
			//else
			//{
			//	Html.MessageBox.Show(Extensions.MessagePage.Message, Extensions.MessagePage.MessageStatus);

			//	Extensions.MessagePage.RemoveMessage();
			//}
		}
	}
}