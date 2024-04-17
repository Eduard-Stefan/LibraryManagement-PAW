using Library_Management.Models;

namespace Library_Management.Repositories.Interfaces
{
	public interface IUserRepository : IRepositoryBase<User>
	{
		IQueryable<User> UnwelcomeUsers();
	}
}