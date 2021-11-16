using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candor.Models
{
	public class IdeaEdit
	{
		[Key]
		public int IdeaId { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Content { get; set; }
		public DateTimeOffset LastModified { get; set; }
		[Required]
		public bool Completed { get; set; }
	}
}
