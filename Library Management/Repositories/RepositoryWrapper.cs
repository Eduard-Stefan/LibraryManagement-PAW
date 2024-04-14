using Library_Management.Models;
using Library_Management.Repositories.Interfaces;

namespace Library_Management.Repositories
{
	public class RepositoryWrapper : IRepositoryWrapper
	{
		private ApplicationDbContext _applicationDbContext;
		private ISubsidiaryRepository? _subsidiaryRepository;
		private IBookRepository? _bookRepository;

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

		public IBookRepository BookRepository
		{
			get
			{
				if (_bookRepository == null)
				{
					_bookRepository = new BookRepository(_applicationDbContext);
				}

				return _bookRepository;
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