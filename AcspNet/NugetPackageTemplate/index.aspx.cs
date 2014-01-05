using System;

using AcspNet;

namespace WebApplication
{
	[LoadExtensionsFromAssemblyOf(typeof(Index))]
	public class Index : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			new Manager(RouteData).Run();
		}
	}
}