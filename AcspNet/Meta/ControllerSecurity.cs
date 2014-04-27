namespace AcspNet.Meta
{
	public class ControllerSecurity
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerSecurity" /> class.
		/// </summary>
		/// <param name="isHttpGet">if set to <c>true</c> then indicates what controller handler only HTTP GET requests.</param>
		/// <param name="isHttpPost">if set to <c>true</c>then indicates what controller handler only HTTP POST requests.</param>
		public ControllerSecurity(bool isHttpGet = false, bool isHttpPost = false)
		{
			IsHttpGet = isHttpGet;
			IsHttpPost = isHttpPost;
		}

		public bool IsHttpGet { get; private set; }
		public bool IsHttpPost { get; private set; }		 
	}
}