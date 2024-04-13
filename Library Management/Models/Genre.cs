﻿namespace Library_Management.Models
{
	public class Genre
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public ICollection<Book> Books { get; set; }
	}
}