using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candor.Data
{
	public class Rating
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public Guid UserId { get; set; }

		[ForeignKey("Idea")]
		public int IdeaId { get; set; }
		public virtual Idea Idea { get; set; }
		[Required]
		public DateTimeOffset DateCreated { get; set; }
		[Required]
		public int RatingScore { get; set; }
		public string Comment { get; set; }
	}
}
