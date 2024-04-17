using System.Linq.Expressions;
using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_Management.Repositories
{
	public class BorrowedBookRepository : RepositoryBase<BorrowedBook>, IBorrowedBookRepository
	{
		public BorrowedBookRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{
		}

		public IQueryable<BorrowedBook> FindAll()
		{
			return ApplicationDbContext.BorrowedBooks.AsNoTracking()
				.Include(b => b.BookSubsidiary)
				.ThenInclude(bs => bs.Book)
				.Include(b => b.BookSubsidiary)
				.ThenInclude(bs => bs.Subsidiary)
				.Include(b => b.User);
		}

		public IQueryable<BorrowedBook> FindByCondition(Expression<Func<BorrowedBook, bool>> expression)
		{
			return ApplicationDbContext.BorrowedBooks.Where(expression).AsNoTracking()
				.Include(b => b.BookSubsidiary)
				.ThenInclude(bs => bs.Book)
				.Include(b => b.BookSubsidiary)
				.ThenInclude(bs => bs.Subsidiary)
				.Include(b => b.User);
		}
	}
}