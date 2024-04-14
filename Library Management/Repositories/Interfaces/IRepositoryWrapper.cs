using Microsoft.Identity.Client;

namespace Library_Management.Repositories.Interfaces
{
	public interface IRepositoryWrapper
	{
		ISubsidiaryRepository SubsidiaryRepository { get; }
		IBookRepository BookRepository { get; }
		IUserRepository UserRepository { get; }
		IAuthorRepository AuthorRepository { get; }

		void Save();
	}
}