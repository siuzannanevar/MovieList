using System;

namespace MovieList.Models
{
	public class Film
	{
		public Guid Id { get; set; } = Guid.NewGuid(); // uniqe Movie id (Guid - Globally Unique Identifier)
		public string Title { get; set; }
		public string Director { get; set; }      
		public int Year { get; set; }  
		public Genre Genre { get; set; }
		public double Rating { get; set; }   
	}
}