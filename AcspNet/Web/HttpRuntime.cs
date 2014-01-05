namespace AcspNet.Web
{
	/// <summary>
	/// System.Web.HttpRuntime wrapper
	/// </summary>
	public class HttpRuntime : IHttpRuntime
	{
		/// <returns>
		/// The virtual path of the directory that contains the application hosted in the current application domain.
		/// </returns>
		public string AppDomainAppVirtualPath
		{
			get { return System.Web.HttpRuntime.AppDomainAppVirtualPath; }
		}
	}
}