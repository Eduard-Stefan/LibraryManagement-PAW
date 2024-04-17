using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_Management.Repositories
{
	public class UserRepository : RepositoryBase<User>, IUserRepository
	{
		public UserRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{
		}

		public IQueryable<User> UnwelcomeUsers()
		{
			var currentDate = DateTime.Now;
			ApplicationDbContext.Users.AsNoTracking()
				.Where(user => user.BorrowedBooks.Any(borrowedBook => borrowedBook.ReturnDate < currentDate))
				.ToList()
				.ForEach(user => user.IsWelcome = false);

			ApplicationDbContext.SaveChanges();

			return ApplicationDbContext.Users.AsNoTracking()
				.Where(user => user.IsWelcome == false);
		}
	}
}