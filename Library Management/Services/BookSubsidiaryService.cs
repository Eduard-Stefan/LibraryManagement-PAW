using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;

namespace Library_Management.Services
{
	public class BookSubsidiaryService : IBookSubsidiaryService
	{
		private IRepositoryWrapper _repositoryWrapper;

		public BookSubsidiaryService(IRepositoryWrapper repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		public List<BookSubsidiary> FindAll()
		{
			return _repositoryWrapper.BookSubsidiaryRepository.FindAll().ToList();
		}

		public BookSubsidiary? FindById(int id)
		{
			return _repositoryWrapper.BookSubsidiaryRepository.FindByCondition(e => e.Id == id).FirstOrDefault();
		}

		public BookSubsidiary? FindByBookIdAndSubsidiaryId(int bookId, int subsidiaryId)
		{
			return _repositoryWrapper.BookSubsidiaryRepository.FindByCondition(e => e.BookId == bookId && e.SubsidiaryId == subsidiaryId).FirstOrDefault();
		}

		public List<BookSubsidiary> FindByBookIdAndQuantityGreaterThanZero(int bookId)
		{
			return _repositoryWrapper.BookSubsidiaryRepository.FindByCondition(e => e.BookId == bookId && e.Quantity > 0).ToList();
		}

		public void Create(BookSubsidiary bookSubsidiary)
		{
			_repositoryWrapper.BookSubsidiaryRepository.Create(bookSubsidiary);
			_repositoryWrapper.Save();
		}

		public void Update(BookSubsidiary bookSubsidiary)
		{
			_repositoryWrapper.BookSubsidiaryRepository.Update(bookSubsidiary);
			_repositoryWrapper.Save();
		}

		public void Delete(BookSubsidiary bookSubsidiary)
		{
			_repositoryWrapper.BookSubsidiaryRepository.Delete(bookSubsidiary);
			_repositoryWrapper.Save();
		}
	}
}