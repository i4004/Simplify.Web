using System;
using System.Linq.Expressions;
using AcspNet.Examples.Domain.Entities;

namespace AcspNet.Examples.Domain
{
	public interface IUsersRepository
	{
		User GetUser(Expression<Func<User, bool>> filterFunc);
		void Update(User user);
	}
}