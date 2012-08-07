/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/

IF EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Mobile' 
	AND	[column].name = 'TotalOutgoingFreeBudget'
) BEGIN


ALTER TABLE dbo.Mobile
	DROP COLUMN TotalOutgoingFreeBudget, TotalOutgoingFreeAdvanced, TotalOutgoingFreePremier, TotalOutgoingFreePremierPorted, TotalOutgoingFreePremierPlus, TotalOutgoingPremium25p, TotalOutgoingPremium50p, TotalOutgoingPremium100p, TotalOutgoingPremium150p, TotalIncomingMms

END
