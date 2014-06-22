using System;

namespace AcspNet.Examples.Domain.Entities
{
	public class User
	{
		public virtual int ID { get; set; }

		public virtual string Name { get; set; }

		public virtual string Password { get; set; }

		public virtual string EMail { get; set; }

		public virtual DateTime LastActivityTime { get; set; }
	}
}