namespace AcspNet.Extensions
{
	/// <summary>
	/// Additional AcspNet extensions instances container class
	/// </summary>
	public class ExtensionsWrapper : IExtensions
	{
		internal IMessagePage MessagePageInstance;
		internal IIdProcessor IdProcessorInstance;

		/// <summary>
		/// Gets the message page that is used to display messages to user on a separated site page.
		/// </summary>
		public IMessagePage MessagePage
		{
			get { return MessagePageInstance; }
		}

		/// <summary>
		/// Gets the identifier processor that is used to parse 'ID' field from request query string or form.
		/// </summary>
		public IIdProcessor IdProcessor
		{
			get { return IdProcessorInstance; }
		}
	}
}