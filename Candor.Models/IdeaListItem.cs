using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candor.Models
{
	public class IdeaListItem
	{
		{
		public int IdeaId { get; set; }
		public string Title { get; set; }
		public DateTimeOffset DateCreated { get; set; }
		public virtual double Ratings { get; set; }
		public bool Completed { get; set; }  
	}
}
}
