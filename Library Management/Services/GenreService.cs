using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;

namespace Library_Management.Services
{
	public class GenreService : IGenreService
	{
		private IRepositoryWrapper _repositoryWrapper;

		public GenreService(IRepositoryWrapper repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		public List<Genre> FindAll()
		{
			return _repositoryWrapper.GenreRepository.FindAll().ToList();
		}

		public Genre? FindById(int id)
		{
			return _repositoryWrapper.GenreRepository.FindByCondition(e => e.Id == id).FirstOrDefault();
		}

		public void Create(Genre genre)
		{
			_repositoryWrapper.GenreRepository.Create(genre);
			_repositoryWrapper.Save();
		}

		public void Update(Genre genre)
		{
			_repositoryWrapper.GenreRepository.Update(genre);
			_repositoryWrapper.Save();
		}

		public void Delete(Genre genre)
		{
			_repositoryWrapper.GenreRepository.Delete(genre);
			_repositoryWrapper.Save();
		}
	}
}