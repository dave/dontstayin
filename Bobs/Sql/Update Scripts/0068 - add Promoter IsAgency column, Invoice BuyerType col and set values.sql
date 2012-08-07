IF NOT EXISTS(
	SELECT * FROM sys.columns c INNER JOIN sys.tables t ON c.object_id = t.object_id WHERE t.Name = 'Promoter' AND c.Name = 'IsAgency'
) BEGIN

	ALTER TABLE dbo.Promoter ADD
	IsAgency bit NOT NULL CONSTRAINT DF_Promoter_IsAgency DEFAULT 0

	
	DECLARE @v sql_variant 
	SET @v = N'if the promoter is an agency or not'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'IsAgency'
	
END

IF EXISTS(
	SELECT * FROM sys.columns c INNER JOIN sys.tables t ON c.object_id = t.object_id WHERE t.Name = 'InsertionOrderItem' AND c.Name = 'Rate'
) BEGIN
	ALTER TABLE dbo.InsertionOrderItem	DROP COLUMN Rate
	
	
	
	
 
	
END

	
GO
UPDATE Promoter SET IsAgency = 1 WHERE ClientSector = 13


GO


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

GO
update inv set BuyerType = 
	case p.IsAgency 
		when 1 then 1	--AgencyPromoter
		else 2			--NonAgencyPromoter
	end
from invoice inv inner join promoter p on inv.promoterk = p.k

update inv set BuyerType = 3 --TicketUsr
	from invoice inv inner join invoiceitem invi on inv.k = invi.invoicek and BuyableObjectType = 18 -- Ticket

update inv set BuyerType = 4 --NonTicketUsr
	from invoice inv left join invoiceitem invi on inv.k = invi.invoicek and BuyableObjectType = 18 -- Ticket
where promoterk = 0 and invi.k is null

