using Library_Management.Models;

namespace Library_Management.Services.Interfaces
{
	public interface IBookService
	{
		List<Book> FindAll();
		Book? FindById(int id);
		void Create(Book book);
		void Update(Book book);
		void Delete(Book book);
	}
}