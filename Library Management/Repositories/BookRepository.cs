using System.Linq.Expressions;
using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Library_Management.Repositories
{
	public class BookRepository : RepositoryBase<Book>, IBookRepository
	{
		public BookRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{
		}

		public IQueryable<Book> FindAll()
		{
			return ApplicationDbContext.Books.AsNoTracking()
				.Include(b => b.Author)
				.Include(b => b.Genre)
				.Include(b => b.Publisher);
		}

		public IQueryable<Book> FindByCondition(Expression<Func<Book, bool>> expression)
		{
			return ApplicationDbContext.Books.Where(expression).AsNoTracking()
				.Include(b => b.Author)
				.Include(b => b.Genre)
				.Include(b => b.Publisher);
		}
	}
}