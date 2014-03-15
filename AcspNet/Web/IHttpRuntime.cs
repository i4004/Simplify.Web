
namespace AcspNet.Web
{
	/// <summary>
	/// System.Web.HttpRuntime wrapper interface
	/// </summary>
	public interface IHttpRuntime : IHideObjectMembers
	{
		/// <returns>
		/// The virtual path of the directory that contains the application hosted in the current application domain.
		/// </returns>
		string AppDomainAppVirtualPath { get; }
	}
}