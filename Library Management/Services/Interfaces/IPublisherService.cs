using Library_Management.Models;

namespace Library_Management.Services.Interfaces
{
	public interface IPublisherService
	{
		List<Publisher> FindAll();
		Publisher? FindById(int id);
		void Create(Publisher publisher);
		void Update(Publisher publisher);
		void Delete(Publisher publisher);
	}
}