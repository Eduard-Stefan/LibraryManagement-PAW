using System.Linq.Expressions;
using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_Management.Repositories
{
	public class BookSubsidiaryRepository : RepositoryBase<BookSubsidiary>, IBookSubsidiaryRepository
	{
		public BookSubsidiaryRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{
		}

		public IQueryable<BookSubsidiary> FindAll()
		{
			return ApplicationDbContext.BooksSubsidiaries.AsNoTracking()
				.Include(bs => bs.Book)
				.Include(bs => bs.Subsidiary);
		}

		public IQueryable<BookSubsidiary> FindByCondition(Expression<Func<BookSubsidiary, bool>> expression)
		{
			return ApplicationDbContext.BooksSubsidiaries.Where(expression).AsNoTracking()
				.Include(bs => bs.Book)
				.Include(bs => bs.Subsidiary);
		}
	}
}