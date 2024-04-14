using Library_Management.Models;
using Library_Management.Repositories.Interfaces;

namespace Library_Management.Repositories
{
	public class RepositoryWrapper : IRepositoryWrapper
	{
		private ApplicationDbContext _applicationDbContext;
		private ISubsidiaryRepository? _subsidiaryRepository;
		private IBookRepository? _bookRepository;
		private IUserRepository? _userRepository;
		private IAuthorRepository? _authorRepository;
		private IPublisherRepository? _publisherRepository;
		private IGenreRepository? _genreRepository;

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

		public IUserRepository UserRepository
		{
			get
			{
				if (_userRepository == null)
				{
					_userRepository = new UserRepository(_applicationDbContext);
				}

				return _userRepository;
			}
		}

		public IAuthorRepository AuthorRepository
		{
			get
			{
				if (_authorRepository == null)
				{
					_authorRepository = new AuthorRepository(_applicationDbContext);
				}

				return _authorRepository;
			}
		}

		public IPublisherRepository PublisherRepository
		{
			get
			{
				if (_publisherRepository == null)
				{
					_publisherRepository = new PublisherRepository(_applicationDbContext);
				}

				return _publisherRepository;
			}
		}

		public IGenreRepository GenreRepository
		{
			get
			{
				if (_genreRepository == null)
				{
					_genreRepository = new GenreRepository(_applicationDbContext);
				}

				return _genreRepository;
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