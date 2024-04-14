using Library_Management.Models;

namespace Library_Management.Services.Interfaces
{
	public interface IAuthorService
	{
		List<Author> FindAll();
		Author? FindById(int id);
		void Create(Author author);
		void Update(Author author);
		void Delete(Author author);
	}
}