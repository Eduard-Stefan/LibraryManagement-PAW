using Library_Management.Models;
using Library_Management.Repositories.Interfaces;

namespace Library_Management.Repositories
{
	public class SubsidiaryRepository : RepositoryBase<Subsidiary>, ISubsidiaryRepository
	{
		public SubsidiaryRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{
		}
	}
}