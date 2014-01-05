using AcspNet.Authentication;

namespace AcspNet.Extensions.Executable
{
	/// <summary>
	/// Class that is used to control extensions from unauthorized users acces
	/// </summary>
	[Priority(-10)]
	[Version("1.0")]
	public sealed class ExtensionsProtector : ExecExtension
	{
		/// <summary>
		/// Invokes the executable extension.
		/// </summary>
		public override void Invoke()
		{
			foreach (var type in Manager.ExecExtensionsTypes)
			{
				var attributes = type.GetCustomAttributes(typeof(ProtectionAttribute), false);

				if (attributes.Length <= 0) continue;

				var protectionType = ((ProtectionAttribute) attributes[0]).ProtectionType;

				if ( protectionType == Protection.User && !AuthenticationModule.IsAuthenticatedAsUser)
					Extensions.MessagePage.NavigateToMessagePage(StringTable["PageDenyErrorUser"]);
			}
		}
	}
}

