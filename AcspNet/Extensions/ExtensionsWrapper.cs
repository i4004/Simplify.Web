namespace AcspNet.Extensions
{
	public class ExtensionsWrapper : IExtensions
	{
		internal MessagePage MessagePageInstance;

		public MessagePage MessagePage
		{
			get { return MessagePageInstance; }
		}
	}
}