using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Repositories;
using Library_Management.Services;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Add(new ServiceDescriptor(typeof(ILog), new ConsoleLogger()));

builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

builder.Services.AddScoped<ISubsidiaryRepository, SubsidiaryRepository>();
builder.Services.AddScoped<ISubsidiaryService, SubsidiaryService>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IPublisherService, PublisherService>();

builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGenreService, GenreService>();

builder.Services.AddScoped<IBookSubsidiaryRepository, BookSubsidiaryRepository>();
builder.Services.AddScoped<IBookSubsidiaryService, BookSubsidiaryService>();

builder.Services.AddScoped<IBorrowedBookRepository, BorrowedBookRepository>();
builder.Services.AddScoped<IBorrowedBookService, BorrowedBookService>();

builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
	options.UseSqlServer(connectionString);
});

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
