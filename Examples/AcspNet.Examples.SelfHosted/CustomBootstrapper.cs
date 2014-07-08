using System;
using AcspNet.Bootstrapper;

namespace AcspNet.Examples.SelfHosted
{
	public class CustomBootstrapper : BaseAcspNetBootstrapper
	{
		public CustomBootstrapper()
		{
			SetRequestHandlerType<CustomRequestHandler>();
		}
	}
}