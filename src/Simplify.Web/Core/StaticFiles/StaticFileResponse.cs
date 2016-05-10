using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Simplify.System;

namespace Simplify.Web.Core.StaticFiles
{
	public class StaticFileResponse : IStaticFileResponse
	{
		private readonly IOwinResponse _response;
		private readonly IResponseWriter _responseWriter;

		public StaticFileResponse(IOwinResponse response, IResponseWriter responseWriter)
		{
			_response = response;
			_responseWriter = responseWriter;
		}

		public Task SendNotModified(DateTime lastModifiedTime)
		{
			SetModificationHeaders(lastModifiedTime);

			_response.StatusCode = 304;
			return Task.Delay(0);
		}

		public Task SendNew(byte[] data, DateTime lastModifiedTime)
		{
			SetModificationHeaders(lastModifiedTime);

			_response.Expires = new DateTimeOffset(TimeProvider.Current.Now.AddYears(1));
			return _responseWriter.WriteAsync(data, _response);
		}

		private void SetModificationHeaders(DateTime lastModifiedTime)
		{
			_response.Headers.Append("Last-Modified", lastModifiedTime.ToString("r"));
		}

		public void SetMimeType(string fileName)
		{
			fileName = fileName.ToLower();

			if (fileName.EndsWith(".css"))
				_response.ContentType = "text/css";
			else if (fileName.EndsWith(".js"))
				_response.ContentType = "text/javascript";
		}
	}
}