using System.Threading.Tasks;
using Simplify.Templates;

namespace AcspNet.Examples.SelfHosted.Views.Shared
{
	public class LoggedUserPanelView : View
	{
		public async Task<ITemplate> Get(string userName)
		{
			var tpl = await TemplateFactory.LoadAsync("Shared/LoginPanel/LoggedUserPanel");

			tpl.Add("UserName", userName);

			return tpl;
		}
	}
}