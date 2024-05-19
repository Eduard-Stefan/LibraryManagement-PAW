using Library_Management.Models;

namespace Library_Management.Services.Interfaces
{
	public interface IBorrowedBookService
	{
		List<BorrowedBook> FindAll();
		List<BorrowedBook> FindAllByUserId(string userId);
		BorrowedBook? FindById(int id);
		BorrowedBook? FindByBookIdAndUserId(int bookId, string userId);
		void Create(BorrowedBook borrowedBook);
		void Update(BorrowedBook borrowedBook);
		void Delete(BorrowedBook borrowedBook);
	}
}