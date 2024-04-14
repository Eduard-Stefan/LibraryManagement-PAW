using Library_Management.Models;

namespace Library_Management.Services.Interfaces
{
	public interface IGenreService
	{
		List<Genre> FindAll();
		Genre? FindById(int id);
		void Create(Genre genre);
		void Update(Genre genre);
		void Delete(Genre genre);
	}
}