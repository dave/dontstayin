using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DomainNameRegistrar.ResponseParsers
{
	abstract class Response
	{
		internal string Xml { get; private set; }
		internal Response(string xml)
		{
			Xml = xml;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			InstantiateFromXml(xmlDocument);
			if (!Validate()) throw new InvalidResponseException(xml);
		}
		internal abstract void InstantiateFromXml(XmlDocument xmlDocument);
		internal abstract bool Validate();

		internal static Availability GetAvailability(int availability)
		{
			return Enum.IsDefined(typeof(Availability), availability) ? (Availability)availability : Availability.UnknownAvailabilityCode;
		}
	}

	class InvalidResponseException : Exception
	{
		public InvalidResponseException(string xml) : base("Response XML did not pass Validation. XML:\n\n" + xml) { }
	}

	#region enums
	enum ResponseCode
	{
		// General
		Success = 1000,
		Failure = 1001,
		RequestTooBig = 1002,

		// Credential Authentications
		LoginDeniedAccountAtConnectionLimit = 1500,
		LoginDeniedInvalidAccountIpAddress = 1501,
		LoginDeniedAccountInactive = 1502,

		// Poll
		NoMessagesWaiting = 1003,
		MessagesWaiting = 1004,

		// Database Error
		Empty = 2000,
		RequiredFieldMissing = 2001,
		PatternMatchingError = 2002,
		FieldTooLong = 2003,
		DbMatchingError = 2004
	}
	enum Availability
	{
		CouldNotFindAvailabilityInfo = -1,
		NotAvailableForRegistration = 0,
		AvailableForRegistration = 1,
		UnknownAvailabilityCode = 2
	}
	#endregion

}
