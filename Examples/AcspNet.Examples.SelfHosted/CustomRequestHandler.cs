using System.Threading.Tasks;
using Microsoft.Owin;

namespace AcspNet.Examples.SelfHosted
{
	public class CustomRequestHandler : IRequestHandler
	{
		public Task ProcessRequest(IOwinContext context)
		{
			throw new System.NotImplementedException();
		}
	}
}