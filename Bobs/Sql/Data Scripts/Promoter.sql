
DELETE FROM [Promoter]
TRUNCATE TABLE [Promoter]

SET IDENTITY_INSERT [Promoter] ON

insert into [Promoter] (K, Name, UrlName) values (1, 'Promoter1', 'Promoter1')
																																																																																																														--(		K,		Name,		ContactName,	PhoneNumber,		CreditLimit,	AddressCountryK,	PricingMultiplier,	DateTimeSignUp, Status, TotalPaid, RenewalFee, RenewalMonths,	UrlName, EnabledDateTime, SalesUsrK, SalesStatus, SalesStatusExpires, SalesNextCall, LetterType, LetterStatus, AccessCodeRandom, OfferType, ClientSector, EnableTickets, AddedByUsrK, AddedMethod,			OverrideApplyTicketFundsToInvoices, IsAgency
INSERT INTO Promoter	(K,		Name,	ContactName,	PhoneNumber,	CreditLimit, AddressCountryK, PricingMultiplier, DateTimeSignUp, Status, TotalPaid, RenewalFee, RenewalMonths, UrlName, EnabledDateTime, SalesUsrK, SalesStatus, SalesStatusExpires, SalesNextCall, LetterType, LetterStatus, AccessCodeRandom, OfferType, ClientSector, EnableTickets, AddedByUsrK, AddedMethod, OverrideApplyTicketFundsToInvoices, IsAgency) VALUES					(2,		'Promoter2', 'P Promoter',	'07845123456',		10000,			224,				1,					getdate(),		3,		0,			0,			0,				'Promoter2',	getdate(),		1,			3,				'31 Dec 9999', getdate(),		1,			1,			12345678,			1,			1,				1,			1,				2,			0,									0)
INSERT INTO Promoter	(K,		Name,	ContactName,	PhoneNumber,	CreditLimit, AddressCountryK, PricingMultiplier, DateTimeSignUp, Status, TotalPaid, RenewalFee, RenewalMonths, UrlName, EnabledDateTime, SalesUsrK, SalesStatus, SalesStatusExpires, SalesNextCall, LetterType, LetterStatus, AccessCodeRandom, OfferType, ClientSector, EnableTickets, AddedByUsrK, AddedMethod, OverrideApplyTicketFundsToInvoices, IsAgency) VALUES					(3,		'Agency1',	'A Agent',		'07654321098',		10000,			224,				1,					getdate(),		3,		0,			0,			0,				'Agency1',		getdate(),		1,			3,				'31 Dec 9999', getdate(),		1,			1,			23456781,			1,			1,				1,			1,				2,			0,									1)
INSERT INTO Promoter	(K,		Name,	ContactName,	PhoneNumber,	CreditLimit, AddressCountryK, PricingMultiplier, DateTimeSignUp, Status, TotalPaid, RenewalFee, RenewalMonths, UrlName, EnabledDateTime, SalesUsrK, SalesStatus, SalesStatusExpires, SalesNextCall, LetterType, LetterStatus, AccessCodeRandom, OfferType, ClientSector, EnableTickets, AddedByUsrK, AddedMethod, OverrideApplyTicketFundsToInvoices, IsAgency) VALUES					(4,		'SuperPromoter','S Promoter','07654321098',		10000,			224,				1,					getdate(),		3,		0,			0,			0,				'SuperPromoter',getdate(),		1,			3,				'31 Dec 9999', getdate(),		1,			1,			34567812,			1,			1,				1,			1,				2,			0,									1)


SET IDENTITY_INSERT [Promoter] OFF
 
 
 
 
 
 
