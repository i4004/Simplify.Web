using NUnit.Framework;

namespace AcspNet.Tests.Extensions.Executable
{
	public class HtrmlListsTest : ExecExtension
	{
		public override void Invoke()
		{
			Assert.AreEqual("<option value=''>Default label</option>", Html.ListsGenerator.GenerateDefaultListItem(false));
			Assert.AreEqual("<option value='' selected='selected'>Default label</option>", Html.ListsGenerator.GenerateDefaultListItem());
			Assert.AreEqual("<option value='0' selected='selected'>0</option><option value='1'>1</option>", Html.ListsGenerator.GenerateNumbersList(2));
		}
	}
}