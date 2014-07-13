namespace AcspNet
{
	/// <summary>
	/// Represent HTTP route parser and matcher
	/// </summary>
	public interface IRouteParser
	{
		void ParseRoute(string route);
	}
}