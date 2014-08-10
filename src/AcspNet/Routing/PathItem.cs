namespace AcspNet.Routing
{
	public abstract class PathItem : IPathItem
	{
		protected PathItem(string name)
		{
			Name = name;
		}

		public string Name { get; private set; }		 
	}
}