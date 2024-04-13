namespace Library_Management.Models
{
	public class BookSubsidiary
	{
		public int Id { get; set; }
		public int Quantity { get; set; }
		public int BookId { get; set; }
		public Book? Book { get; set; }
		public int SubsidiaryId { get; set; }
		public Subsidiary? Subsidiary { get; set; }
		public ICollection<BorrowedBook>? BorrowedBooks { get; set; }
	}
}