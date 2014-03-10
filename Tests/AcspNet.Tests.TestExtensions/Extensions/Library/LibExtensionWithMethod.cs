namespace AcspNet.Tests.TestExtensions.Extensions.Library
{
	public class LibExtensionWithMethod : LibExtension
	{
		private int _number;

		public override void Initialize()
		{
			_number = 256;
		}

		public int GetNumber()
		{
			return _number;
		}
	}
}