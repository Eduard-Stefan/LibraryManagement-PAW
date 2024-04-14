using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;

namespace Library_Management.Services
{
	public class UserService : IUserService
	{
		private IRepositoryWrapper _repositoryWrapper;

		public UserService(IRepositoryWrapper repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		public List<User> FindAll()
		{
			return _repositoryWrapper.UserRepository.FindAll().ToList();
		}

		public User? FindById(string id)
		{
			return _repositoryWrapper.UserRepository.FindByCondition(e => e.Id == id).FirstOrDefault();
		}

		public void Update(User user)
		{
			_repositoryWrapper.UserRepository.Update(user);
			_repositoryWrapper.Save();
		}

		public void Delete(User user)
		{
			_repositoryWrapper.UserRepository.Delete(user);
			_repositoryWrapper.Save();
		}
	}
}