using System.Collections.Generic;

namespace AcspNet.Routing
{
	public class ControllerPath : IControllerPath
	{
		public ControllerPath(IList<IPathItem> items)
		{
			Items = items;
		}

		public IList<IPathItem> Items { get; private set; }
	}
}