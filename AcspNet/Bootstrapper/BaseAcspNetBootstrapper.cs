using System;

namespace AcspNet.Bootstrapper
{
	public class BaseAcspNetBootstrapper
	{
		private Type _requestHandlerType;

		public Type RequestHandlerType
		{
			get { return _requestHandlerType ?? typeof(RequestHandler); }
		}

		public void SetRequestHandler<T>()
			where T : IRequestHandler
		{
			_requestHandlerType = typeof(T);
		}
	}
}