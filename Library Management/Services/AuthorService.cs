using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;

namespace Library_Management.Services
{
	public class AuthorService : IAuthorService
	{
		private IRepositoryWrapper _repositoryWrapper;

		public AuthorService(IRepositoryWrapper repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		public List<Author> FindAll()
		{
			return _repositoryWrapper.AuthorRepository.FindAll().ToList();
		}

		public Author? FindById(int id)
		{
			return _repositoryWrapper.AuthorRepository.FindByCondition(e => e.Id == id).FirstOrDefault();
		}

		public void Create(Author author)
		{
			_repositoryWrapper.AuthorRepository.Create(author);
			_repositoryWrapper.Save();
		}

		public void Update(Author author)
		{
			_repositoryWrapper.AuthorRepository.Update(author);
			_repositoryWrapper.Save();
		}

		public void Delete(Author author)
		{
			_repositoryWrapper.AuthorRepository.Delete(author);
			_repositoryWrapper.Save();
		}
	}
}