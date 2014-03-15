namespace AcspNet.Extensions
{
	/// <summary>
	/// Additional AcspNet extensions instances container class
	/// </summary>
	public class ExtensionsWrapper : IExtensionsWrapper
	{
		//internal IMessagePage MessagePageInstance;
		///// <summary>
		///// Gets the message page that is used to display messages to user on a separated site page.
		///// </summary>
		//public IMessagePage MessagePage
		//{
		//	get { return MessagePageInstance; }
		//}

		/// <summary>
		/// Gets the identifier processor that is used to parse 'ID' field from request query string or form.
		/// </summary>
		public IIdVerifier IdVerifier { get; internal set; }

		/// <summary>
		/// Website navigation manager, controls current user location, link to previous page or link specific page
		/// </summary>
		public INavigator Navigator { get; internal set; }
	}
}