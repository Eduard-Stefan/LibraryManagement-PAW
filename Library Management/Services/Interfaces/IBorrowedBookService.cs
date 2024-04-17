using Library_Management.Models;

namespace Library_Management.Services.Interfaces
{
	public interface IBorrowedBookService
	{
		List<BorrowedBook> FindAll();
		BorrowedBook? FindById(int id);
		void Delete(BorrowedBook borrowedBook);
	}
}