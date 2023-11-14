using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTP
{
	public class Person
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Credit_Score { get; set; }
		public bool Married { get; set; }

		public override string ToString()
		{
			return $"{Id} - {Name} - {Credit_Score} - {Married}";
		}
	}
}
