namespace Library_Management.Models
{
	public class Subsidiary
	{
		public int Id { get; set; }
		public string Name { get; set; } = "";
		public string Address { get; set; } = "";
		public ICollection<BookSubsidiary>? BookSubsidiaries { get; set; }
	}
}