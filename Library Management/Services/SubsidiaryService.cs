using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;

namespace Library_Management.Services
{
	public class SubsidiaryService : ISubsidiaryService
	{
		private IRepositoryWrapper _repositoryWrapper;

		public SubsidiaryService(IRepositoryWrapper repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		public List<Subsidiary> FindAll()
		{
			return _repositoryWrapper.SubsidiaryRepository.FindAll().ToList();
		}

		public Subsidiary? FindById(int id)
		{
			return _repositoryWrapper.SubsidiaryRepository.FindByCondition(e => e.Id == id).FirstOrDefault();
		}

		public void Create(Subsidiary subsidiary)
		{
			_repositoryWrapper.SubsidiaryRepository.Create(subsidiary);
			_repositoryWrapper.Save();
		}

		public void Update(Subsidiary subsidiary)
		{
			_repositoryWrapper.SubsidiaryRepository.Update(subsidiary);
			_repositoryWrapper.Save();
		}

		public void Delete(Subsidiary subsidiary)
		{
			_repositoryWrapper.SubsidiaryRepository.Delete(subsidiary);
			_repositoryWrapper.Save();
		}
	}
}