using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;
using Bobs;

namespace Bobs.BannerServer.Rules.TypesOfRule
{
	class IdentityPropertyRules : Rule
	{
		bool[] values = new bool[125];

		internal IdentityPropertyRules(Identity id)
		{
			SetGenderTargettingBitInfoType(id.Usr);
			SetAgeRangeTargetting(id.Usr);
			SetIsPromoterTargettingBitInfoType(id.Usr);

			SetDemographics(id.Demographics);
		}

		private void SetDemographics(Demographics demographics)
		{
			if (demographics != null)
			{
				SetDemograpicsBitsInfoType(
					demographics.Salary,
					Banner.TargettingProperty.Salary_Unknown,
					Banner.TargettingProperty.Salary_1,
					Banner.TargettingProperty.Salary_2,
					Banner.TargettingProperty.Salary_3,
					Banner.TargettingProperty.Salary_4,
					Banner.TargettingProperty.Salary_5,
					Banner.TargettingProperty.Salary_6,
					Banner.TargettingProperty.Salary_7
					);

				SetDemograpicsBitsInfoType(
					demographics.Employment,
					Banner.TargettingProperty.Employment_Unknown,
					Banner.TargettingProperty.Employment_1,
					Banner.TargettingProperty.Employment_2,
					Banner.TargettingProperty.Employment_3,
					Banner.TargettingProperty.Employment_4
					);

				SetDemograpicsBitsInfoType(
					demographics.DrinkAlcopops,
					Banner.TargettingProperty.DrinkAlcopops_True,
					Banner.TargettingProperty.DrinkAlcopops_False
					);

				SetDemograpicsBitsInfoType(
					demographics.DrinkBottledBeer,
					Banner.TargettingProperty.DrinkBottledBeer_True,
					Banner.TargettingProperty.DrinkBottledBeer_False
					);

				SetDemograpicsBitsInfoType(
					demographics.DrinkCider,
					Banner.TargettingProperty.DrinkCider_True,
					Banner.TargettingProperty.DrinkCider_False
					);

				SetDemograpicsBitsInfoType(
					demographics.DrinkDraftBeer,
					Banner.TargettingProperty.DrinkDraftBeer_True,
					Banner.TargettingProperty.DrinkDraftBeer_False
					);

				SetDemograpicsBitsInfoType(
					demographics.DrinkEnergy,
					Banner.TargettingProperty.DrinkEnergy_True,
					Banner.TargettingProperty.DrinkEnergy_False
					);

				SetDemograpicsBitsInfoType(
					demographics.DrinkSoft,
					Banner.TargettingProperty.DrinkSoft_True,
					Banner.TargettingProperty.DrinkSoft_False
					);

				SetDemograpicsBitsInfoType(
					demographics.DrinkSpirits,
					Banner.TargettingProperty.DrinkSpirits_True,
					Banner.TargettingProperty.DrinkSpirits_False
					);

				SetDemograpicsBitsInfoType(
					demographics.DrinkWater,
					Banner.TargettingProperty.DrinkWater_True,
					Banner.TargettingProperty.DrinkWater_False
					);

				SetDemograpicsBitsInfoType(
					demographics.DrinkWine,
					Banner.TargettingProperty.DrinkWine_True,
					Banner.TargettingProperty.DrinkWine_False
					);

				SetDemograpicsBitsInfoType(
					demographics.CreditCard,
					Banner.TargettingProperty.CreditCard_True,
					Banner.TargettingProperty.CreditCard_False
					);

				SetDemograpicsBitsInfoType(
					demographics.Loan,
					Banner.TargettingProperty.Loan_True,
					Banner.TargettingProperty.Loan_False
					);

				SetDemograpicsBitsInfoType(
					demographics.Mortgage,
					Banner.TargettingProperty.Mortgage_True,
					Banner.TargettingProperty.Mortgage_False
					);

				SetDemograpicsBitsInfoType(
					demographics.SpendMusicCd,
					Banner.TargettingProperty.SpendMusicCd_Zero,
					Banner.TargettingProperty.SpendMusicCd_Zero, // "Nothing" is actually 1
					Banner.TargettingProperty.SpendMusicCd_MoreThanZero,
					Banner.TargettingProperty.SpendMusicCd_MoreThanZero,
					Banner.TargettingProperty.SpendMusicCd_MoreThanZero,
					Banner.TargettingProperty.SpendMusicCd_MoreThanZero,
					Banner.TargettingProperty.SpendMusicCd_MoreThanZero,
					Banner.TargettingProperty.SpendMusicCd_MoreThanZero
					);

				SetDemograpicsBitsInfoType(
					demographics.SpendMusicVinyl,
					Banner.TargettingProperty.SpendMusicVinyl_Zero,
					Banner.TargettingProperty.SpendMusicVinyl_Zero, // "Nothing" is actually 1
					Banner.TargettingProperty.SpendMusicVinyl_MoreThanZero,
					Banner.TargettingProperty.SpendMusicVinyl_MoreThanZero,
					Banner.TargettingProperty.SpendMusicVinyl_MoreThanZero,
					Banner.TargettingProperty.SpendMusicVinyl_MoreThanZero,
					Banner.TargettingProperty.SpendMusicVinyl_MoreThanZero,
					Banner.TargettingProperty.SpendMusicVinyl_MoreThanZero
					);

				SetDemograpicsBitsInfoType(
					demographics.SpendMusicDownload,
					Banner.TargettingProperty.SpendMusicDownload_Zero,
					Banner.TargettingProperty.SpendMusicDownload_Zero, // "Nothing" is actually 1
					Banner.TargettingProperty.SpendMusicDownload_MoreThanZero,
					Banner.TargettingProperty.SpendMusicDownload_MoreThanZero,
					Banner.TargettingProperty.SpendMusicDownload_MoreThanZero,
					Banner.TargettingProperty.SpendMusicDownload_MoreThanZero,
					Banner.TargettingProperty.SpendMusicDownload_MoreThanZero,
					Banner.TargettingProperty.SpendMusicDownload_MoreThanZero
					);
			}
			else
			{
				Add(Banner.TargettingProperty.Salary_Unknown);
				Add(Banner.TargettingProperty.Employment_Unknown);
				Add(Banner.TargettingProperty.DrinkAlcopops_Unknown);
				Add(Banner.TargettingProperty.DrinkBottledBeer_Unknown);
				Add(Banner.TargettingProperty.DrinkCider_Unknown);
				Add(Banner.TargettingProperty.DrinkDraftBeer_Unknown);
				Add(Banner.TargettingProperty.DrinkEnergy_Unknown);
				Add(Banner.TargettingProperty.DrinkSoft_Unknown);
				Add(Banner.TargettingProperty.DrinkSpirits_Unknown);
				Add(Banner.TargettingProperty.DrinkWater_Unknown);
				Add(Banner.TargettingProperty.DrinkWine_Unknown);
				Add(Banner.TargettingProperty.CreditCard_Unknown);
				Add(Banner.TargettingProperty.Loan_Unknown);
				Add(Banner.TargettingProperty.Mortgage_Unknown);
				Add(Banner.TargettingProperty.SpendMusicCd_Unknown);
				Add(Banner.TargettingProperty.SpendMusicVinyl_Unknown);
				Add(Banner.TargettingProperty.SpendMusicDownload_Unknown);
			}
		}

		public override Q Q
		{
			get
			{
				long[] targettingProperties = new long[2];
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i]) targettingProperties[i / 63] += 1L << (i % 63);
				}
				return new And(
								new Q(Banner.Columns.TargettingProperties0, QueryOperator.BitwiseAndEqualsZero, targettingProperties[0]),
								new Q(Banner.Columns.TargettingProperties1, QueryOperator.BitwiseAndEqualsZero, targettingProperties[1])
						   );
			}
		}

		internal IdentityPropertyRules Add(Banner.TargettingProperty targettingBit)
		{
			values[(int)targettingBit] = true;
			return this;
		}

		private void SetGenderTargettingBitInfoType(Usr usr)
		{
			if (usr == null || usr.IsFemale == usr.IsMale)
			{
				Add(Banner.TargettingProperty.Gender_Unknown);
			}
			else if (usr.IsMale)
			{
				Add(Banner.TargettingProperty.Gender_Male);
			}
			else
			{
				Add(Banner.TargettingProperty.Gender_Female);
			}
		}

		private void SetAgeRangeTargetting(Usr usr)
		{
			if (usr == null || usr.DateOfBirth == null || usr.DateOfBirth == DateTime.MinValue)
			{
				Add(Banner.TargettingProperty.AgeRange_Unknown);
			}
			else
			{
				int ageInYears = CalculateAgeInYears(Common.Time.Today, usr.DateOfBirth);
				if (ageInYears < 18)
				{
					Add(Banner.TargettingProperty.AgeRange_Under_18);
				}
				else if (ageInYears < 25)
				{
					Add(Banner.TargettingProperty.AgeRange_18_24);
				}
				else if (ageInYears < 30)
				{
					Add(Banner.TargettingProperty.AgeRange_25_29);
				}
				else if (ageInYears < 35)
				{
					Add(Banner.TargettingProperty.AgeRange_30_34);
				}
				else if (ageInYears < 40)
				{
					Add(Banner.TargettingProperty.AgeRange_35_39);
				}
				else if (ageInYears < 45)
				{
					Add(Banner.TargettingProperty.AgeRange_40_44);
				}
				else if (ageInYears < 50)
				{
					Add(Banner.TargettingProperty.AgeRange_45_49);
				}
				else
				{
					Add(Banner.TargettingProperty.AgeRange_50_Plus);
				}
			}
		}

		private int CalculateAgeInYears(DateTime today, DateTime birthday)
		{
			int years = today.Year - birthday.Year;
			// subtract another year if we're before the birth day in the current year
			if (today.Month < birthday.Month || (today.Month == birthday.Month && today.Day < birthday.Day))
				years--;
			return years;
		}

		private void SetIsPromoterTargettingBitInfoType(Usr usr)
		{
			if (usr == null)
			{
				// don't add either bit
			}
			else if (usr.IsPromoter)
			{
				Add(Banner.TargettingProperty.IsPromoter_True);
			}
			else
			{
				Add(Banner.TargettingProperty.IsPromoter_False);
			}
		}

		private void SetDemograpicsBitsInfoType(bool value, Banner.TargettingProperty trueProperty, Banner.TargettingProperty falseProperty)
		{
			Add(value ? trueProperty : falseProperty);
		}

		private void SetDemograpicsBitsInfoType(int value, params Banner.TargettingProperty[] properties)
		{
			Add(properties[value]);
		}

	}
}

