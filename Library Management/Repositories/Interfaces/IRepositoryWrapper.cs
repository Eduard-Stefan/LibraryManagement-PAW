namespace Library_Management.Repositories.Interfaces
{
	public interface IRepositoryWrapper
	{
		ISubsidiaryRepository SubsidiaryRepository { get; }

		void Save();
	}
}