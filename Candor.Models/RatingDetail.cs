using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candor.Models
{
	public class RatingDetail
	{
		public string UserName { get; set; }
		[Display(Name = "Rating ID")]
		public int RatingId { get; set; }
		[Display(Name = "Idea ID")]
		public int IdeaId { get; set; }
		[Display(Name = "Rating Score")]
		public int RatingScore { get; set; }
		public string Comment { get; set; }
		public bool IsEditable { get; set; }
	}
}

