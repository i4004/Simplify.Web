using System.Threading.Tasks;
using Simplify.Templates;

namespace AcspNet.Examples.SelfHosted.Views
{
	public class LoginPanelView : View
	{
		public async Task<ITemplate> Get(string userName)
		{
			var tpl = await TemplateFactory.LoadAsync("Shared/LoginPanel");

			tpl.Add("UserName", userName);

			return tpl;
		}
	}
}