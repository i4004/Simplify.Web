using System;
using Simplify.DI;
using Simplify.DI.Provider.SimpleInjector;
using Simplify.Web.Examples.WindowsService.Setup;
using Simplify.WindowsServices;

namespace Simplify.Web.Examples.WindowsService
{
	internal class Program
	{
		public static string[] Args { get; private set; }

		private static void Main(string[] args)
		{
#if DEBUG
			global::System.Diagnostics.Debugger.Launch();
#endif

			Args = args;

			InitializeContainer();

			if (new BasicServiceHandler<WebApplicationStartup>().Start(args))
				return;

			using (var scope = DIContainer.Current.BeginLifetimeScope())
				scope.Resolver.Resolve<WebApplicationStartup>().Run();

			Console.ReadLine();
		}

		private static void InitializeContainer()
		{
			var provider = new SimpleInjectorDIProvider();
			DIContainer.Current = provider;

			IocRegistrations.Register();

			provider.Container.Verify();
		}
	}
}