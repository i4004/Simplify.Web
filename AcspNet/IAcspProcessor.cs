namespace AcspNet
{
	/// <summary>
	/// Creates and executes ACSP extensions for current HTTP request
	/// </summary>
	public interface IAcspProcessor : IHideObjectMembers
	{
		/// <summary>
		/// Creates and executes ACSP extensions for current HTTP request
		/// </summary>
		void Execute();
	}
}