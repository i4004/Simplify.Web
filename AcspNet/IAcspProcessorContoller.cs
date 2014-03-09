namespace AcspNet
{
	public interface IAcspProcessorContoller : IHideObjectMembers
	{
		/// <summary>
		/// Stop ACSP subsequent extensions execution
		/// </summary>
		void StopExtensionsExecution();
	}
}