using Microsoft.Owin;

namespace Simplify.Web.Core.StaticFiles
{
	public interface IStaticFileResponseFactory
	{
		IStaticFileResponse Create(IOwinResponse response);
	}
}