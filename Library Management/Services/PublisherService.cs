using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;

namespace Library_Management.Services
{
	public class PublisherService : IPublisherService
	{
		private IRepositoryWrapper _repositoryWrapper;

		public PublisherService(IRepositoryWrapper repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		public List<Publisher> FindAll()
		{
			return _repositoryWrapper.PublisherRepository.FindAll().ToList();
		}

		public Publisher? FindById(int id)
		{
			return _repositoryWrapper.PublisherRepository.FindByCondition(e => e.Id == id).FirstOrDefault();
		}

		public void Create(Publisher publisher)
		{
			_repositoryWrapper.PublisherRepository.Create(publisher);
			_repositoryWrapper.Save();
		}

		public void Update(Publisher publisher)
		{
			_repositoryWrapper.PublisherRepository.Update(publisher);
			_repositoryWrapper.Save();
		}

		public void Delete(Publisher publisher)
		{
			_repositoryWrapper.PublisherRepository.Delete(publisher);
			_repositoryWrapper.Save();
		}
	}
}