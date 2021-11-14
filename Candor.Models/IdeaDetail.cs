using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candor.Models
{
    public class IdeaDetail
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTimeOffset DateCreated { get; set; }
		public DateTimeOffset LastModified { get; set; }
		public virtual double Ratings { get; set; }
		public Double AverageRating { get; set; }
		public bool Completed { get; set; }
	}
}
