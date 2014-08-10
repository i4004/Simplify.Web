namespace AcspNet.Routing
{
	public interface IControllerPathParser
	{
		IControllerPath Parse(string controllerPath);
	}
}