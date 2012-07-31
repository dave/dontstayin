using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Reflection;
using System.Data.SqlClient;
using Common;

namespace CommonTests
{
	[TestFixture]
	public class Settings_Tests : UnitTestUtilities.DatabaseRollbackTestClass
	{
		//[Test]
		//public void SettingsCanBeReadFromTheDatabaseUsingSettingsClass()
		//{
		//    UnitTestUtilities.Sql.SqlHelper.ClearTable(Common.Properties.ConnectionString, "Setting");
		//    Settings.RefreshAll();

		//    SqlConnection conn = new SqlConnection(Properties.ConnectionString);
		//    SqlCommand cmd = new SqlCommand("INSERT INTO setting (Name, Value) VALUES (@name, @value)", conn);
		//    cmd.Parameters.AddWithValue("@name", "BannerServerMethod");
		//    cmd.Parameters.AddWithValue("@value", (int)Settings.BannerVisibilityInDevEnvOption.Visible);

		//    try
		//    {
		//        conn.Open();
		//        cmd.ExecuteNonQuery();
		//    }
		//    finally
		//    {
		//        conn.Close();
		//    }

		//    Assert.AreEqual(Settings.BannerVisibilityInDevEnvOption.Visible, Settings.BannerVisibilityInDevEnv);
		//}

		//[Test]
		//public void SettingValueDoesNotChangeIfTableIsChangedWithoutUsingTheSettingsClassConfirmingThatItIsCached()
		//{
		//    SettingsCanBeReadFromTheDatabaseUsingSettingsClass();

		//    UpdateSettingDirectlyInDatabase("BannerServerMethod", (int)Settings.BannerVisibilityInDevEnvOption.Hidden);

		//    Assert.AreEqual(Settings.BannerVisibilityInDevEnvOption.Visible, Settings.BannerVisibilityInDevEnv);
		//}

		private static void UpdateSettingDirectlyInDatabase(string name, object value)
		{
			SqlConnection conn = new SqlConnection(Properties.ConnectionString);
			SqlCommand cmd = new SqlCommand("UPDATE setting SET value = @value WHERE name = @name", conn);
			cmd.Parameters.AddWithValue("@name", name);
			cmd.Parameters.AddWithValue("@value", value);
			try
			{
				conn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
			}
		}

		//[Test]
		//public void SettingValueChangesIfTableIsChangedUsingTheSettingsClassConfirmingThatTheCacheIsInvalidated()
		//{
		//    SettingsCanBeReadFromTheDatabaseUsingSettingsClass();
		//    Settings.BannerVisibilityInDevEnv = Settings.BannerVisibilityInDevEnvOption.Hidden;
		//    Assert.AreEqual(Settings.BannerVisibilityInDevEnvOption.Hidden, Settings.BannerVisibilityInDevEnv);
		//}

		//[Test]
		//public void RefreshAllSettingsGetsLatestFromDatabase()
		//{
		//    SettingsCanBeReadFromTheDatabaseUsingSettingsClass();
		//    UpdateSettingDirectlyInDatabase("BannerServerMethod", (int)Settings.BannerVisibilityInDevEnvOption.Hidden);

		//    Settings.RefreshAll();
		//    Assert.AreEqual(Settings.BannerVisibilityInDevEnvOption.Hidden, Settings.BannerVisibilityInDevEnv);
		//}

		[Test]
		public void CheckEachSettingCanCreateDefaultCorrespondingDatabaseEntry()
		{
			foreach (MethodInfo mi in Common.Reflection.TypeExtensions.GetGetters(typeof(Settings)))
			{
				if (mi.ReturnType.BaseType == typeof(Enum))
				{
					int? enumValue = null;
					try
					{
						enumValue = (int)Common.Reflection.OptimizedMethodCall.BuildOptimizedDelegate(mi).Invoke(null, null);
					}
					catch
					{
						Assert.Fail("There is no database setting for " + mi.Name.Substring(4));
					}
					Assert.IsTrue(Enum.IsDefined(mi.ReturnType, enumValue.Value), "Setting value of '" + enumValue.Value + "' for " + mi.Name.Substring(4) + " is not suitable for enum " + mi.ReturnType.ToString());
				}
				else if (mi.ReturnType == typeof(string))
				{
					Assert.IsNotNull(Common.Reflection.OptimizedMethodCall.BuildOptimizedDelegate(mi).Invoke(null, null), "There is no database setting for " + mi.Name.Substring(4));
				}
				else if (mi.ReturnType == typeof(bool))
				{
					Assert.IsNotNull(Common.Reflection.OptimizedMethodCall.BuildOptimizedDelegate(mi).Invoke(null, null), "There is no database setting for " + mi.Name.Substring(4));
				}
				else
				{
					Assert.Fail("Unexpected property type: " + mi.ReturnType.ToString());
				}
			}
		}
	}
}
