namespace Library_Management.Models
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; } = "";
		public Author Author { get; set; }
		public int AuthorId { get; set; }
		public Publisher Publisher { get; set; }
		public int PublisherId { get; set; }
		public Genre Genre { get; set; }
		public int GenreId { get; set; }
		public string ImageFileName { get; set; } = "";
		public ICollection<BookSubsidiary>? BookSubsidiaries { get; set; }
	}
}