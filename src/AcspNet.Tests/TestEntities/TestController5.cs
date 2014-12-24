using System.Threading.Tasks;

namespace AcspNet.Tests.TestEntities
{
	public class TestController5 : AsyncController<TestModel>
	{
		public override Task<ControllerResponse> Invoke()
		{
			throw new System.NotImplementedException();
		}
	}
}