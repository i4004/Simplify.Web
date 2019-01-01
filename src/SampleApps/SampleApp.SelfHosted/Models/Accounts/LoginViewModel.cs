using Simplify.Web.ModelBinding.Attributes;

namespace SampleApp.SelfHosted.Models.Accounts
{
	public class LoginViewModel
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}