using System;
using AcspNet.Examples.Domain;
using Simplify.Log;

namespace AcspNet.Examples.Controllers
{
	//[ViewModel(typeof(LoginViewModel))]
	//[ViewModel<LoginViewModel>()]

	[Action("processLogin")]
	[Ajax]
	public class LoginController : Controller
	{
		private readonly IUsersService _usersService;

		//CurrentViewModel

		public LoginController(IUsersService usersService)
		{
			_usersService = usersService;
		}

		public override void Invoke()
		{
			try
			{
				// var model = (LoginViewModel)CurrentViewModel;
				//var user = _usersService.GetUser(mode.UserName, mode.UserPassword);

				var user = _usersService.GetUser("", "");

				if (user != null)
				{
					//manager.LogInSessionUser(user.ID);

					//dataCollector.DisplayPartial("1");
				}
				//else
					//dataCollector.DisplayPartial(messageBox.GetSmall(st["NotifyAuthenticationError"]));
			}
			catch (Exception e)
			{
				Logger.Default.Write(e);
				//dataCollector.DisplayPartial(messageBox.GetSmall(st["NotifyUnexpectedSiteError"]));
			}
		}
	}
}