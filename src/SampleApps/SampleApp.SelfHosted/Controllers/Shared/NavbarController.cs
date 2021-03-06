﻿using Simplify.Web;
using Simplify.Web.Responses;

namespace SampleApp.SelfHosted.Controllers.Shared
{
	public class NavbarController : Controller
	{
		public override ControllerResponse Invoke()
		{
			return new InlineTpl("Navbar", TemplateFactory.Load("Navbar"));
		}
	}
}