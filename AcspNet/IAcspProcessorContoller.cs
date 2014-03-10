namespace AcspNet
{
	public interface IAcspProcessorContoller : IHideObjectMembers
	{
		/// <summary>
		/// Stop ACSP subsequent extensions execution
		/// </summary>
		void StopExtensionsExecution();

		/// <summary>
		/// Prevent data sent to displayer to be displayed
		/// </summary>
		void DisableDisplay();
	}
}