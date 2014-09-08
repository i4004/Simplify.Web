using System.Threading.Tasks;

namespace AcspNet.Tests.TestEntities
{
	public class TestController5 : AsyncController<TestModelString>
	{
		public override Task<ControllerResponse> Invoke()
		{
			throw new System.NotImplementedException();
		}
	}
}