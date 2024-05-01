using Library_Management.Models;
using Library_Management.Repositories;
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

		public List<BorrowedBook> FindAllByUserId(string userId)
		{
			return _repositoryWrapper.BorrowedBookRepository.FindByCondition(e => e.UserId == userId).ToList();
		}

		public BorrowedBook? FindById(int id)
		{
			return _repositoryWrapper.BorrowedBookRepository.FindByCondition(e => e.Id == id).FirstOrDefault();
		}

		public void Create(BorrowedBook borrowedBook)
		{
			_repositoryWrapper.BorrowedBookRepository.Create(borrowedBook);
			_repositoryWrapper.Save();
		}

		public void Update(BorrowedBook borrowedBook)
		{
			_repositoryWrapper.BorrowedBookRepository.Update(borrowedBook);
			_repositoryWrapper.Save();
		}

		public void Delete(BorrowedBook borrowedBook)
		{
			_repositoryWrapper.BorrowedBookRepository.Delete(borrowedBook);
			_repositoryWrapper.Save();
		}
	}
}