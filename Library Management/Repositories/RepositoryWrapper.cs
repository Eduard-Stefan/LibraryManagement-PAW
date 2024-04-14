using Library_Management.Models;
using Library_Management.Repositories.Interfaces;

namespace Library_Management.Repositories
{
	public class RepositoryWrapper : IRepositoryWrapper
	{
		private ApplicationDbContext _applicationDbContext;
		private ISubsidiaryRepository? _subsidiaryRepository;

		public ISubsidiaryRepository SubsidiaryRepository
		{
			get
			{
				if (_subsidiaryRepository == null)
				{
					_subsidiaryRepository = new SubsidiaryRepository(_applicationDbContext);
				}

				return _subsidiaryRepository;
			}
		}

		public RepositoryWrapper(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public void Save()
		{
			_applicationDbContext.SaveChanges();
		}
	}
}