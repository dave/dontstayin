IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'TicketRun' and c.Name='RequiresDeliveryAddress') BEGIN
	ALTER TABLE dbo.TicketRun DROP COLUMN RequiresDeliveryAddress
END
GO
IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'TicketRun' and c.Name='DeliveryMethod') BEGIN
	ALTER TABLE dbo.TicketRun DROP COLUMN DeliveryMethod 
	ALTER TABLE dbo.TicketRun DROP COLUMN DeliveryCharge
	
END
GO
IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Ticket' and c.Name='DeliveryMethod') BEGIN
	ALTER TABLE dbo.Ticket DROP COLUMN DeliveryMethod 
	ALTER TABLE dbo.Ticket DROP COLUMN DeliveryCharge
	
END
GO
IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Ticket' and c.Name='DeliveryName') BEGIN
	ALTER TABLE dbo.Ticket DROP COLUMN DeliveryName
	
END

GO
IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Ticket' and c.Name='AddressName') BEGIN
	ALTER TABLE dbo.Ticket DROP COLUMN AddressName
	
END

GO
ALTER TABLE dbo.TicketRun ADD DeliveryMethod INT NULL
ALTER TABLE dbo.TicketRun ADD DeliveryCharge decimal(6,2) NULL
ALTER TABLE dbo.Ticket ADD AddressName VARCHAR(150) NULL



go
	EXECUTE sp_addextendedproperty N'MS_Description', N'Delivery method for the tickets', 
		N'SCHEMA', N'dbo', N'TABLE', N'TicketRun', N'COLUMN', N'DeliveryMethod'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Delivery charge for deliverinh the tickets', 
		N'SCHEMA', N'dbo', N'TABLE', N'TicketRun', N'COLUMN', N'DeliveryCharge'
 	EXECUTE sp_addextendedproperty N'MS_Description', N'Name of the person to deliver the tickets to', 
		N'SCHEMA', N'dbo', N'TABLE', N'Ticket', N'COLUMN', N'AddressName'
 

GO
UPDATE TicketRun SET DeliveryMethod = 1, DeliveryCharge = 4.95, DeliveryDate = '11 AUg 08' WHERE K IN (1643, 1644) and db_name() = 'db_spotted'
