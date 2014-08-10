using System;

namespace AcspNet.Routing
{
	public class PathParameter : PathItem
	{
		public Type Type { get; set; }

		public PathParameter(string name, Type type) : base(name)
		{
			Type = type;
		}
	}
}