using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Library_Management.Models;
using Library_Management.Repositories.Interfaces;

namespace Library_Management.Repositories
{
	public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		protected ApplicationDbContext ApplicationDbContext { get; set; }

		public RepositoryBase(ApplicationDbContext applicationDbContext)
		{
			this.ApplicationDbContext = applicationDbContext;
		}

		public IQueryable<T> FindAll()
		{
			return this.ApplicationDbContext.Set<T>().AsNoTracking();
		}

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
		{
			return this.ApplicationDbContext.Set<T>().Where(expression).AsNoTracking();
		}

		public void Create(T entity)
		{
			this.ApplicationDbContext.Set<T>().Add(entity);
		}

		public void Update(T entity)
		{
			this.ApplicationDbContext.Set<T>().Update(entity);
		}

		public void Delete(T entity)
		{
			this.ApplicationDbContext.Set<T>().Remove(entity);
		}
	}
}