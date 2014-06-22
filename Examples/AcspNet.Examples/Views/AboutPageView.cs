namespace AcspNet.Examples.Views
{
	public class AboutPageView : View
	{
		public string Get()
		{
			return TemplateFactory.Load("AboutPage.tpl").Get();			
		}
	}
}