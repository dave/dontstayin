
IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Ticket' and c.Name='DeliveryAddressStreet') BEGIN
	ALTER TABLE dbo.Ticket DROP COLUMN DeliveryAddressStreet
END
GO
IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Ticket' and c.Name='DeliveryAddressArea') BEGIN
	ALTER TABLE dbo.Ticket DROP COLUMN DeliveryAddressArea
	
END
GO
IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Ticket' and c.Name='DeliveryAddressTown') BEGIN
	ALTER TABLE dbo.Ticket DROP COLUMN DeliveryAddressTown
END
GO
IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Ticket' and c.Name='DeliveryAddressCounty') BEGIN
	ALTER TABLE dbo.Ticket DROP COLUMN DeliveryAddressCounty
END
GO
IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Ticket' and c.Name='DeliveryAddressPostcode') BEGIN
	ALTER TABLE dbo.Ticket DROP COLUMN DeliveryAddressPostcode
END
GO
IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Ticket' and c.Name='DeliveryAddressCountryK') BEGIN
	ALTER TABLE dbo.Ticket DROP COLUMN DeliveryAddressCountryK
	
END

GO
IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'TicketRun' and c.Name='DeliveryDate') BEGIN
	ALTER TABLE dbo.TicketRun DROP COLUMN DeliveryDate
	
END
GO
ALTER TABLE dbo.TicketRun ADD DeliveryDate DATETIME NULL


GO
EXECUTE sp_addextendedproperty N'MS_Description', N'Approximate date tickets usrs will be told tickets will be delivered', N'SCHEMA', N'dbo', N'TABLE', N'TicketRun', N'COLUMN', N'DeliveryDate'

GO
IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Transfer' and c.Name='CardAddress2') BEGIN
	ALTER TABLE dbo.Transfer DROP COLUMN CardAddress2
END
GO
IF EXISTS(SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Transfer' and c.Name='CardAddressArea') BEGIN
	ALTER TABLE dbo.Transfer DROP COLUMN CardAddressArea  
	ALTER TABLE dbo.Transfer DROP COLUMN CardAddressTown  
	ALTER TABLE dbo.Transfer DROP COLUMN CardAddressCounty 
	ALTER TABLE dbo.Transfer DROP COLUMN CardAddressCountryK  
END
GO
ALTER TABLE dbo.Transfer ADD CardAddressArea VARCHAR(50)
ALTER TABLE dbo.Transfer ADD CardAddressTown VARCHAR(50)
ALTER TABLE dbo.Transfer ADD CardAddressCounty VARCHAR(50)
ALTER TABLE dbo.Transfer ADD CardAddressCountryK INT
GO
EXECUTE sp_addextendedproperty N'MS_Description', N'Part of address card is registered to', N'SCHEMA', N'dbo', N'TABLE', N'Transfer', N'COLUMN', N'CardAddressArea'	
EXECUTE sp_addextendedproperty N'MS_Description', N'Part of address card is registered to', N'SCHEMA', N'dbo', N'TABLE', N'Transfer', N'COLUMN', N'CardAddressTown'
EXECUTE sp_addextendedproperty N'MS_Description', N'Part of address card is registered to', N'SCHEMA', N'dbo', N'TABLE', N'Transfer', N'COLUMN', N'CardAddressCounty'
EXECUTE sp_addextendedproperty N'MS_Description', N'Part of address card is registered to', N'SCHEMA', N'dbo', N'TABLE', N'Transfer', N'COLUMN', N'CardAddressCountryK'


GO

