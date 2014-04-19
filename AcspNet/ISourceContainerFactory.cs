namespace AcspNet
{
	/// <summary>
	/// Represents a factory for source containers creation
	/// </summary>
	public interface ISourceContainerFactory : IHideObjectMembers
	{
		/// <summary>
		/// Creates the source container.
		/// </summary>
		/// <returns></returns>
		SourceContainer CreateContainer();
	}
}