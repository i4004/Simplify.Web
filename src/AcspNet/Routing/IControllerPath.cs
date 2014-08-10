using System.Collections.Generic;

namespace AcspNet.Routing
{
	public interface IControllerPath
	{
		IList<IPathItem> Items { get; }
	}
}