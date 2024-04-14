using Library_Management.Models;

namespace Library_Management.Services.Interfaces
{
	public interface IBookSubsidiaryService
	{
		List<BookSubsidiary> FindAll();
		BookSubsidiary? FindById(int id);
		void Create(BookSubsidiary bookSubsidiary);
		void Update(BookSubsidiary bookSubsidiary);
		void Delete(BookSubsidiary bookSubsidiary);
	}
}