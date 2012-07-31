using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using Bobs;

namespace DnsConfigBatchFilesGenerator
{
	public class Generator
	{
		// TODO: put these in config file
		static readonly string filesDirectory = @"DnsConfigBatchFilesGeneratorFiles\";
		static readonly string addDontStayInAliasesFilename = "Add-DontStayIn-Aliases.bat";
		static readonly string refreshAllFilename = "RefreshDomains-DSI.bat";
		static readonly string primaryServer = "EXTRA";
		static readonly string secondaryServer = "MACE";

		static void Main(string[] args)
		{
			DomainSet domains = GetDomainsRequiringRegistration();

			if (domains.Count > 0)
			{
				GenerateAliasesBatchFile(domains);
				if (!Common.Properties.IsDevelopmentEnvironment)
				{
					InvokeBatchFile();
				}
				UpdateRegisteredFlag(domains);
			}
		}

		static Domain.Columns DomainRegisteredColumn
		{
			get
			{
				string machineNameUpperCase = Common.Properties.MachineName.ToUpper();
				if (machineNameUpperCase == primaryServer.ToUpper())
				{
					return Domain.Columns.RegisteredPrimary;
				}
				if (machineNameUpperCase == secondaryServer.ToUpper())
				{
					return Domain.Columns.RegisteredSecondary;
				}
				throw new Exception(string.Format("This process is only designed to run on {0} or {1}", primaryServer, secondaryServer));
			}
		}
		
		private static DomainSet GetDomainsRequiringRegistration()
		{
			Query q = new Query();
			q.QueryCondition = new Or(new Q(DomainRegisteredColumn, QueryOperator.IsNull), new Q(DomainRegisteredColumn, false));
			return new DomainSet(q);
		}

		private static void UpdateRegisteredFlag(DomainSet ds)
		{
			foreach (Domain d in ds)
			{
				string machineNameUpperCase = Common.Properties.MachineName.ToUpper();
				if (machineNameUpperCase == primaryServer.ToUpper())
				{
					d.RegisteredPrimary = true;
				}
				if (machineNameUpperCase == secondaryServer.ToUpper())
				{
					d.RegisteredSecondary = true;
				}
				d.Update();
			}
		}

		private static void GenerateAliasesBatchFile(DomainSet domains)
		{
			StreamWriter sw = File.CreateText(filesDirectory + addDontStayInAliasesFilename);
			sw.Write(GetAliasesBatchFileContent(domains));
			sw.Flush();
			sw.Close();
		}

		#region
		private static string GetAliasesBatchFileContent(DomainSet domains)
		{
			StringBuilder sb = new StringBuilder(@"
SET PRIMARY_SERVER_NAME=%1
SET PRIMARY_DOMAIN=%2
SET PRIMARY_IP=%3
SET SECONDRY_SERVER_NAME=%4
SET SECONDRY_DOMAIN=%5
SET SECONDRY_IP=%6
SET SOA_EMAIL=%7

");
			// for padding to make file a bit more readable
			int maxLength = 0;
			foreach (Domain d in domains)
			{
				if (d.DomainName.Length > maxLength) maxLength = d.DomainName.Length;
			}
			foreach (Domain d in domains)
			{
				sb.Append("CALL AddAliasDomain %PRIMARY_SERVER_NAME% %PRIMARY_DOMAIN% %PRIMARY_IP% %SECONDRY_SERVER_NAME% %SECONDRY_DOMAIN% %SECONDRY_IP% %SOA_EMAIL% ");
				sb.Append(d.DomainName.PadRight(maxLength));
				sb.AppendLine(" 84.45.14.100 www.dontstayin.com");
			}

			sb.AppendLine();
			return sb.ToString();
		}
		#endregion


		private static void InvokeBatchFile()
		{
			Utilities.ExecuteBatchFile(filesDirectory, filesDirectory + refreshAllFilename);
		}
	}
}
