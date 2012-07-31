using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Common.Collections;
using Common;

namespace CommonTests.Collections
{
	[TestFixture]
	public class PriorityListTests
	{
		[Test]
		public void CheckThatAnUnorderedListBecomesOrdered() 
		{
			List<TestPriorityClass> testValues = new List<TestPriorityClass>();
			PriorityList<TestPriorityClass> priorityList = new PriorityList<TestPriorityClass>();
			for (int i = 0; i < 50; i++)
			{
				priorityList.Add(new TestPriorityClass() { Priority = ThreadSafeRandom.Next(30), Value = ThreadSafeRandom.NextDouble().ToString() });
			}
			int startPriority = -1;
			foreach (var pi in priorityList)
			{
				Assert.GreaterOrEqual(pi.Priority, startPriority);
				if (startPriority < pi.Priority) startPriority = pi.Priority;
			}
		}
	}
	class TestPriorityClass : IHasPriority
	{
		public string Value { get; set; }
		public int Priority { get; set; }
	}
}
