using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Bobs.Tests
{
	[TestFixture]
	public class Banner_Tests : UnitTestUtilities.DatabaseRollbackTestClass
	{
		[Test]
		public void TargettingProperties_Set_Get()
		{
			bool[] boolArrayExpected = new bool[126];
			Random r = new Random();
			for (int i = 0; i < boolArrayExpected.Length; i++)
			{
				boolArrayExpected[i] = r.Next(2) == 1;
			}
			Banner b = new Banner();
			b.TargettingPropertiesToExclude = boolArrayExpected;

			bool[] boolArrayActual = b.TargettingPropertiesToExclude;

			for (int i = 0; i < boolArrayExpected.Length; i++)
			{
				Assert.AreEqual(boolArrayExpected[i], boolArrayActual[i], "index : "+i);
			}
		}

		[Test]
		public void TargettingProperties_SetTargettingProperty()
		{
			Banner.TargettingProperty propertyToTest = Banner.TargettingProperty.CreditCard_False;

			Banner b = new Banner();
			Assert.IsFalse(b.TargettingPropertiesToExclude[(int)propertyToTest]);

			b.SetTargettingProperty(propertyToTest, true);
			Assert.IsTrue(b.TargettingPropertiesToExclude[(int)propertyToTest]);

			foreach (Banner.TargettingProperty tp in Enum.GetValues(typeof(Banner.TargettingProperty)))
			{
				if (tp == propertyToTest) { continue; }
				Assert.IsFalse(b.TargettingPropertiesToExclude[(int)tp]);
			}

			b.SetTargettingProperty(propertyToTest, false);
			foreach (Banner.TargettingProperty tp in Enum.GetValues(typeof(Banner.TargettingProperty)))
			{
				Assert.IsFalse(b.TargettingPropertiesToExclude[(int)tp]);
			}
		}

		[Test]
		public void EnsureAllTargettingPropertyEnumValuesAreUnique()
		{
			List<int> values = new List<int>();
			bool unique = true;
			foreach (Banner.TargettingProperty tp in Enum.GetValues(typeof(Banner.TargettingProperty)))
			{
				if (values.Contains((int)tp))
				{
					unique = false; break;
				}
				values.Add((int)tp);
			}
			Assert.IsTrue(unique);
		}

		[Test]
		public void Set_Set_Unset_TargettingProperty()
		{
			Banner.TargettingProperty propertyToTest1 = Banner.TargettingProperty.Gender_Unknown;
			Banner.TargettingProperty propertyToTest2 = Banner.TargettingProperty.CreditCard_Unknown;

			Banner b = new Banner();
			b.SetTargettingProperty(propertyToTest1, true);
			b.SetTargettingProperty(propertyToTest2, true);

			Assert.IsTrue(b.TargettingPropertiesToExclude[(int)propertyToTest1]);
			Assert.IsTrue(b.TargettingPropertiesToExclude[(int)propertyToTest2]);

			b.SetTargettingProperty(propertyToTest1, false);
			Assert.IsFalse(b.TargettingPropertiesToExclude[(int)propertyToTest1]);
		}



		[Test]
		public void TargettingProperties_SetTargettingProperty_TargettingProperties1()
		{
			Banner.TargettingProperty propertyToTest = Banner.TargettingProperty.AgeRange_50_Plus;

			Banner b = new Banner();
			Assert.IsFalse(b.TargettingPropertiesToExclude[(int)propertyToTest]);

			b.SetTargettingProperty(propertyToTest, true);
			Assert.IsTrue(b.TargettingPropertiesToExclude[(int)propertyToTest]);

			foreach (Banner.TargettingProperty tp in Enum.GetValues(typeof(Banner.TargettingProperty)))
			{
				if (tp == propertyToTest) { continue; }
				Assert.IsFalse(b.TargettingPropertiesToExclude[(int)tp]);
			}

			b.SetTargettingProperty(propertyToTest, false);
			foreach (Banner.TargettingProperty tp in Enum.GetValues(typeof(Banner.TargettingProperty)))
			{
				Assert.IsFalse(b.TargettingPropertiesToExclude[(int)tp]);
			}
		}

		[Test]
		public void Set_Set_Unset_TargettingProperties1()
		{
			Banner.TargettingProperty propertyToTest1 = Banner.TargettingProperty.AgeRange_50_Plus;
			Banner.TargettingProperty propertyToTest2 = Banner.TargettingProperty.CreditCard_Unknown;

			Banner b = new Banner();
			b.SetTargettingProperty(propertyToTest1, true);
			b.SetTargettingProperty(propertyToTest2, true);

			Assert.IsTrue(b.TargettingPropertiesToExclude[(int)propertyToTest1]);
			Assert.IsTrue(b.TargettingPropertiesToExclude[(int)propertyToTest2]);

			b.SetTargettingProperty(propertyToTest1, false);
			Assert.IsFalse(b.TargettingPropertiesToExclude[(int)propertyToTest1]);
		}

		[Test]
		public void RefundEndedBanners()
		{
			new Delete(TablesEnum.Banner, new Q(true)).Run();

			int credits = 123;
			Banner b = new Banner()
			{
				FirstDay = DateTime.Today.AddDays(-2),
				LastDay = DateTime.Today.AddDays(-1),
				TotalRequiredImpressions = 1, // any number greater than zero
				PriceCreditsStored = credits,
				Refunded = false,
				StatusBooked = true,
				Position = Banner.Positions.Hotbox
			};
			b.Update();
			Banner.RefundFinishedBanners();
			b = new Banner(b.K);
			Assert.AreEqual(credits, b.RefundedCredits);
		}
	}
}
