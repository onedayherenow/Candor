using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candor.Models
{
	public class RatingCreate
	{	
		[Required]
		[Display(Name = "Idea ID")]
		public int IdeaId { get; set; }
		[Required]
		[Display(Name = "Rating Score")]
		[Range(0, 10, ErrorMessage = "Rating Score is any value from 0 to 10")]
		public int RatingScore { get; set; }
		public string Comment { get; set; }
	}
}
