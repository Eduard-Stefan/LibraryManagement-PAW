using Library_Management.Models;

namespace Library_Management.Services.Interfaces
{
	public interface IUserService
	{
		List<User> FindAll();
		User? FindById(string id);
		void Update(User user);
		void Delete(User user);
		List<User> UnwelcomeUsers();
	}
}