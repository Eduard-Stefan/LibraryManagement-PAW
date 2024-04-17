using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;

namespace Library_Management.Services
{
	public class BorrowedBookService : IBorrowedBookService
	{
		private IRepositoryWrapper _repositoryWrapper;

		public BorrowedBookService(IRepositoryWrapper repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		public List<BorrowedBook> FindAll()
		{
			return _repositoryWrapper.BorrowedBookRepository.FindAll().ToList();
		}

		public BorrowedBook? FindById(int id)
		{
			return _repositoryWrapper.BorrowedBookRepository.FindByCondition(e => e.Id == id).FirstOrDefault();
		}

		public void Delete(BorrowedBook borrowedBook)
		{
			_repositoryWrapper.BorrowedBookRepository.Delete(borrowedBook);
			_repositoryWrapper.Save();
		}
	}
}