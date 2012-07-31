using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
	public class Numbers
	{
		public static decimal RoundUpAPennyIfNecessary(decimal value)
		{
			if (value > Math.Round(value, 2))
			{
				return Math.Round(value, 2) + 0.01m;
			}
			else
			{
				return value;
			}
		}
		public static double RoundUpAPennyIfNecessary(double value)
		{
			if (value > Math.Round(value, 2))
			{
				return Math.Round(value, 2) + 0.01d;
			}
			else
			{
				return value;
			}
		}
	}
}
