using Library_Management.Models;

namespace Library_Management.Services.Interfaces
{
	public interface ISubsidiaryService
	{
		List<Subsidiary> FindAll();
		Subsidiary? FindById(int value);
		void Create(Subsidiary subsidiary);
		void Update(Subsidiary subsidiary);
		void Delete(Subsidiary subsidiary);
	}
}