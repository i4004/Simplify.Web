using System;
using System.Threading.Tasks;

namespace Simplify.Web.Core.StaticFiles
{
	public interface IStaticFileResponse
	{
		Task SendNotModified(DateTime lastModifiedTime);

		Task SendNew(byte[] data, DateTime lastModifiedTime);

		void SetMimeType(string fileName);
	}
}