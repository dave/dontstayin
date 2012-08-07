IF EXISTS(
	SELECT * FROM sys.columns c INNER JOIN sys.tables t ON c.object_id = t.object_id WHERE t.Name = 'Invoice' AND c.Name = 'InvoiceType'
) BEGIN
	alter TABLE Invoice DROP COLUMN InvoiceType
END

GO
	
IF NOT EXISTS(
	SELECT * FROM sys.columns c INNER JOIN sys.tables t ON c.object_id = t.object_id WHERE t.Name = 'Invoice' AND c.Name = 'BuyerType'
) BEGIN

	ALTER TABLE dbo.Invoice ADD BuyerType int

	
	DECLARE @v sql_variant 
	SET @v = N'Type of the buyer: AgencyPromoter = 1, NonAgencyPromoter = 2, TicketUsr = 3, NonTicketUsr = 4'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Invoice', N'COLUMN', N'BuyerType'
	
END
