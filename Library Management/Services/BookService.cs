using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;

namespace Library_Management.Services
{
	public class BookService : IBookService
	{
		private IRepositoryWrapper _repositoryWrapper;

		public BookService(IRepositoryWrapper repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		public List<Book> FindAll()
		{
			return _repositoryWrapper.BookRepository.FindAll().ToList();
		}

		public Book? FindById(int id)
		{
			return _repositoryWrapper.BookRepository.FindByCondition(e => e.Id == id).FirstOrDefault();
		}

		public void Create(Book book)
		{
			_repositoryWrapper.BookRepository.Create(book);
			_repositoryWrapper.Save();
		}

		public void Update(Book book)
		{
			_repositoryWrapper.BookRepository.Update(book);
			_repositoryWrapper.Save();
		}

		public void Delete(Book book)
		{
			_repositoryWrapper.BookRepository.Delete(book);
			_repositoryWrapper.Save();
		}
	}
}