using AcspNet.Examples.SelfHosted.Models.Accounts;
using Simplify.Templates;

namespace AcspNet.Examples.SelfHosted.Views.Accounts
{
	public class LoginView : View
	{
		public ITemplate Get(LoginViewModel viewModel = null)
		{
			return TemplateFactory.Load("Accounts/LoginPage")/*.Set(viewModel)*/;
		}
	}
}