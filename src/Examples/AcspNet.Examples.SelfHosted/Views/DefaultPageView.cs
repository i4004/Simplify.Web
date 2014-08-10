namespace AcspNet.Examples.SelfHosted.Views
{
	public class DefaultPageView : View
	{
		private readonly Foo _foo;

		public DefaultPageView(Foo foo)
		{
			_foo = foo;
		}

		public string Get()
		{
			return _foo.Bar();
		}
	}
}