namespace AcspNet.Extensions.Executable
{
	/// <summary>
	///  Message display page
	/// </summary>
	[Action("message")]
	[Version("1.0")]
	public sealed class MessagePageDisplay : ExecExtension
	{
		/// <summary>
		/// Invokes the executable extension.
		/// </summary>
		public override void Invoke()
		{
			if(string.IsNullOrEmpty(Extensions.MessagePage.Message))
				Manager.Redirect(AcspNet.Manager.SiteVirtualPath + "/");
			else
			{
				Html.MessageBox.Show(Extensions.MessagePage.Message, Extensions.MessagePage.MessageStatus);

				Extensions.MessagePage.RemoveMessage();
			}
		}
	}
}

