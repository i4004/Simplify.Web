namespace AcspNet.Examples.Controllers
{
	[Http404]
	public class Http404Handler : Controller
	{
		public override void Invoke()
		{
			MessageBox.Show("Oops! Page is not found!");
		}
	}
}