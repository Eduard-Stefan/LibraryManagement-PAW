using Library_Management.Models;
using Library_Management.Repositories.Interfaces;

namespace Library_Management.Repositories
{
	public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
	{
		public AuthorRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{
		}
	}
}