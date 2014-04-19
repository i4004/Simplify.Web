namespace AcspNet
{
	/// <summary>
	/// Represents a factory for source containers creation
	/// </summary>
	public interface ISourceContainerFactory : IHideObjectMembers
	{
		SourceContainer CreateContainer();
	}
}