using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candor.Models
{
	public class IdeaListItem
	{
		[Display(Name = "Idea ID")]
		public string UserName { get; set; }
		[Display(Name = "Idea ID")]
		public int IdeaId { get; set; }
		public string Title { get; set; }
		[Display(Name = "Date Created")]
		public DateTimeOffset DateCreated { get; set; }
		[Display(Name = "Average Rating")]
		public double AverageRating { get; set; }
		[UIHint("Completed")]
		public bool Completed { get; set; }
		public bool IsEditable { get; set; }
	}
}

