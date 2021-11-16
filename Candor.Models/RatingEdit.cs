using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candor.Models
{
	public class RatingEdit
	{
		public int RatingId { get; set; }
		[Required]
		public int RatingScore { get; set; }
		public string Comment { get; set; }
	}
}
