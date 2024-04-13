namespace Library_Management.Models
{
	public class BorrowedBook
	{
		public int Id { get; set; }
		public string? UserId { get; set; }
		public User? User { get; set; }
		public int BookSubsidiaryId { get; set; }
		public BookSubsidiary? BookSubsidiary { get; set; }
		public DateTime BorrowedDate { get; set; }
		public DateTime ReturnDate { get; set; }
	}
}