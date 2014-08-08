namespace AcspNet
{
	/// <summary>
	/// Represent controller response
	/// </summary>
	public interface IControllerResponse : IHideObjectMembers
	{
		/// <summary>
		/// Processes this response
		/// </summary>
		void Process();
	}
}