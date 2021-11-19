using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candor.Models
{
	public class RatingListItem
	{
		public string UserName { get; set; }
		public int RatingId { get; set; }
		public int RatingScore { get; set; }
		public string Comment { get; set; }
		public bool IsEditable { get; set; }
	}
}
