namespace AcspNet.Examples.Views
{
	public class LoginPageView : View
	{
		public string Get()
		{
			return TemplateFactory.Load("UserLogin/LoginForm.tpl").Get();
		}
	}
}