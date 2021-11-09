using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candor.Models
{
	public class RatingDetail
	{
		public int Id { get; set; }
		public int IdeaId { get; set; }
		public int RatingScore { get; set; }
		public string Comment { get; set; }

		public string UsernameRater { get; set; }
	}
}

