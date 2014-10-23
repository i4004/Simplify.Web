using System;
using AcspNet.Meta;
using AcspNet.Owin;
using AcspNet.Owin.Security.AesDataProtectorProvider;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Simplify.DI;
using Simplify.DI.Provider.SimpleInjector;

namespace AcspNet.Examples.SelfHosted
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// Exclude AcspNet from exclude assemblies to be able to load example controllers
			AcspTypesFinder.ExcludedAssembliesPrefixes.Remove("AcspNet");

			var provider = new SimpleInjectorDIProvider();
			DIContainer.Current = provider;
			
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/login")
			});

			app.UseAesDataProtectorProvider();

			app.UseAcspNet();

			provider.Container.Verify();

			AcspNetOwinMiddleware.OnException += Ex;
		}

		private static void Ex(Exception e)
		{
			
		}
	}
}