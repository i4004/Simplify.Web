namespace AcspNet
{
	/// <summary>
	/// Represent ACSP processor behaviour controller
	/// </summary>
	public interface IAcspProcessorContoller : IHideObjectMembers
	{
		/// <summary>
		/// Stop ACSP execution
		/// </summary>
		void StopExecution();
	}
}