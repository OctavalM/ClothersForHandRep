using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothersForHand.Date
{
	public partial class Material
	{
		public int IsCurrentMinCount
		{
			get
			{
				if (CountInStock < MinCount)
					return 1;
				else if ((CountInStock / MinCount) * 100 == 300)
					return 2;
				else
					return 3;
			}
		}

		public string Supliers
		{
			get
			{
				return string.Join(", ", PossibleSupliersMaterial.Select(x => x.Suplier.SuplierName));
			}
		}
	}
}
