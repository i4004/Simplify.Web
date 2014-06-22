using AcspNet.Examples.Domain.Entities;

namespace AcspNet.Examples.Domain
{
	public interface IUsersService
	{
		User GetUser(string userName, string password);
	}
}