using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Candor.Data
{
    public class Idea
    {
		[Key]
		public int Id { get; set; }
		[Required]
		public Guid OwnerId { get; set; }

		[Required]
		public string Title { get; set; }
		[Required]
		public string Content { get; set; }

		[Required]
		public DateTimeOffset DateCreated { get; set; }

		public DateTimeOffset LastModified { get; set; }
		public virtual List<Rating> Ratings { get; set; }
		[Required]
		public double AverageRating => Ratings.Any() ? Ratings.Average(rating => rating.RatingScore) : 0;
		[Required]
		public bool Completed { get; set; }
	}
}
