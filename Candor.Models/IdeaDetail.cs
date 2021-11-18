﻿using Candor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candor.Models
{
    public class IdeaDetail
	{
		public int IdeaId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTimeOffset DateCreated { get; set; }
		public DateTimeOffset LastModified { get; set; }
		public virtual Rating Ratings { get; set; }
		public double AverageRating { get; set; }
		public bool Completed { get; set; }
	}
}
