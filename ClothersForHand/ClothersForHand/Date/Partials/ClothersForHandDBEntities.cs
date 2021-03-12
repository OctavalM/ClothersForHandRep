using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothersForHand.Date
{
	public partial class ClothersForHandDBEntities : DbContext
	{
		private static ClothersForHandDBEntities _context;

		public static ClothersForHandDBEntities GetContext()
		{
			if (_context == null)
				_context = new ClothersForHandDBEntities();

			return _context;
		}
	}
}
