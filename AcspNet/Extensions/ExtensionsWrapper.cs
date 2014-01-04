namespace AcspNet.Extensions
{
	public class ExtensionsWrapper : IExtensions
	{
		internal IMessagePage MessagePageInstance;
		internal IIdProcessor IdProcessorInstance;

		public IMessagePage MessagePage
		{
			get { return MessagePageInstance; }
		}

		public IIdProcessor IdProcessor
		{
			get { return IdProcessorInstance; }
		}
	}
}