using Candor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candor.Models
{
    public class IdeaDetail
	{
		public string UserName { get; set; }
		[Display(Name = "Idea ID")]
		public int IdeaId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		[Display(Name = "Date Created")]
		public DateTimeOffset DateCreated { get; set; }
		[Display(Name = "Last Modified")]
		public DateTimeOffset LastModified { get; set; }
		[Display(Name = "Average Rating")]
		public double AverageRating { get; set; }
		public bool Completed { get; set; }

		public List<RatingListItem> Ratings { get; set; }
	}
}
