using System;
using AcspNet.Examples.Domain.Entities;

namespace AcspNet.Examples.Domain
{
	public class UsersService : IUsersService
	{
		private readonly IUsersRepository _repository;

		public UsersService(IUsersRepository repository)
		{
			_repository = repository;
		}

		public User GetUser(string userName, string password)
		{
			if (userName == null) throw new ArgumentNullException("userName");

			return _repository.GetUser(x => x.Name == userName);
		}
	}
}