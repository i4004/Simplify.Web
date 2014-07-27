namespace AcspNet
{
	/// <summary>
	/// Provides controllers responses base class
	/// </summary>
	public abstract class ControllerResponse : IControllerResponse
	{
		public virtual void Process()
		{
			throw new System.NotImplementedException();
		}
	}
}